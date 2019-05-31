using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace Azure.Utils
{
    public class KeyVaultUtil
    {
        private const string authority = "https://login.microsoftonline.com/";
        private string _clientId;
        private string _clientSecret;
        private string _tenantId;
        private string _vaultName;
        private AzureServiceTokenProvider azureServiceTokenProvider;
        private KeyVaultClient keyVaultClient;

        /// <summary>
        /// Constructor para uso quando app hospedado no azure
        /// </summary>
        /// <param name="VaultName">Nome do cofre</param>
        public KeyVaultUtil(string VaultName)
        {
            azureServiceTokenProvider = new AzureServiceTokenProvider();
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            _vaultName = VaultName;
        }

        /// <summary>
        /// Constructor para app não hospedado no azure
        /// </summary>
        /// <param name="VaultName"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tenantId"></param>
        public KeyVaultUtil(string VaultName, string clientId, string clientSecret, string tenantId)
        {
            _vaultName = VaultName;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _tenantId = tenantId;
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));            
        }

        /// <summary>
        /// Recupera um secret do Vault
        /// </summary>
        /// <param name="secretName">Nome do secret no vault</param>
        /// <returns></returns>
        public async Task<string> GetSecret(string secretName)
        {
            var secret = await keyVaultClient.GetSecretAsync($"https://{_vaultName}.vault.azure.net/secrets/{secretName}").ConfigureAwait(false);
            return secret.Value;
        }


        private async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(_clientId, _clientSecret);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Falha ao obter JWT token");

            return result.AccessToken;
        }
    }
}
