using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Actions
{
    public class JwtTokenAction : ActionBase
    {
        /// <summary>
        /// Name of custom entity used to oauth token flow
        /// </summary>
        private const string token_entity = "sad_token";

        /// <summary>
        /// name of attibute used to input token endpoint
        /// ex: "https://login.microsoftonline.com/94b1e746-0c85-4ddd-99b7-1e6ac22c732b/oauth2/token";
        /// </summary>
        private const string token_endpoint_attribute = "sad_token_endpoint";

        /// <summary>
        /// name of attibute used to input ClientId
        /// ex: "5bad115b-8a84-47dc-bae0-831b2b5c90e6";
        /// </summary>
        private const string clientid_attribute = "sad_clientid";

        /// <summary>
        /// name of attibute used to input client secret
        /// ex:"kzzqngba7/*r*D3zlqZwLNnwyH_3nL7*";
        /// </summary>
        private const string client_secret_attribute = "sad_client_secret";

        /// <summary>
        /// Campo data expiracao token
        /// </summary>
        private const string data_expiracao = "sad_data_expiracao";

        /// <summary>
        /// Campo com ultimo token gerado
        /// </summary>
        private const string valor_token = "sad_valor_token";

        /// <summary>
        /// name of attibute used to input resource
        /// </summary>
        private const string resource_attribute = "sad_resource";

        private string[] atributos = new string[]{ "sad_token_endpoint", "sad_clientid", "sad_client_secret",
        "sad_resource","sad_valor_token","sad_data_expiracao" };

        /// <summary>
        /// Define the entity used to maintain token config
        /// </summary>
        [Input("Token Config")]
        [ReferenceTarget(token_entity)]
        public InArgument<EntityReference> TokenConfig { get; set; }


        /// <summary>
        /// JWT token response
        /// </summary>
        [Output("Token")]
        public OutArgument<string> Token { get; set; }

        /// <summary>
        /// JWT token ExpireIn property
        /// </summary>
        [Output("ExpiresIn")]
        public OutArgument<DateTime> ExpiresIn { get; set; }

        /// <summary>
        /// Token result status
        /// </summary>
        [Output("Sucess")]
        public OutArgument<Boolean> Sucess { get; set; }

        /// <summary>
        /// Token result message
        /// </summary>
        [Output("Message")]
        public OutArgument<string> Message { get; set; }


        protected override void OnExecute()
        {            
            Trace("Start....");            
            try
            {
                //get de record used to configure token creation
                EntityReference configRefence = TokenConfig.Get(ExecutionContext);
                if (configRefence == null)
                {
                    Sucess.Set(ExecutionContext, true);
                    Message.Set(ExecutionContext, "Nenhuma referencia de configuração foi definida na Action.");
                    return;
                }
            
                var tokenConfigRecord = Service.Retrieve(configRefence.LogicalName, configRefence.Id, new ColumnSet(atributos));
                ValidarCamposObrigatorios(tokenConfigRecord);
                var dataExpiracao = tokenConfigRecord.Contains(data_expiracao) ? tokenConfigRecord.GetAttributeValue<DateTime>(data_expiracao) : DateTime.MinValue;

                //Obtem novo token se atual expirado
                if (dataExpiracao < DateTime.Now)
                {
                    ObterToken(tokenConfigRecord);
                }
                else
                {
                    Token.Set(ExecutionContext, tokenConfigRecord.GetAttributeValue<DateTime>(valor_token));
                    ExpiresIn.Set(ExecutionContext, tokenConfigRecord.GetAttributeValue<DateTime>(data_expiracao));
                    Sucess.Set(ExecutionContext, true);
                    Message.Set(ExecutionContext, "Sucess");
                }
            }
            catch (Exception ex)
            {
                Message.Set(ExecutionContext, ex.Message);
            }
        }

        private void ObterToken(Entity tokenConfigRecord)
        {
            var client = new HttpClient();
            //get all attibutes required to get token 
            string tokenEndPoint = (string)tokenConfigRecord[token_endpoint_attribute];
            string clientId = (string)tokenConfigRecord[clientid_attribute];
            string clientSecret = (string)tokenConfigRecord[client_secret_attribute];
            string resource = (string)tokenConfigRecord[resource_attribute];

            client.BaseAddress = new Uri(tokenEndPoint);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("resource", resource),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("client_info", "1"),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
             });

            var result = client.PostAsync("", content).GetAwaiter().GetResult();
            string resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (!string.IsNullOrEmpty(resultContent) && resultContent.Contains("access_token"))
            {
                var tokenParse = new TokenParse(resultContent);
                Token.Set(ExecutionContext, tokenParse.access_token);
                var dataExpiracao = DateTime.Now.AddSeconds(tokenParse.expires_in);
                ExpiresIn.Set(ExecutionContext, dataExpiracao);
                Sucess.Set(ExecutionContext, true);
                Message.Set(ExecutionContext, "Sucess");
                //grava token na entidade para cache
                var tokenUpdt = new Entity() { LogicalName = tokenConfigRecord.LogicalName, Id = tokenConfigRecord.Id };
                tokenUpdt[data_expiracao] = dataExpiracao;
                tokenUpdt[valor_token] = tokenParse.access_token;
                Service.Update(tokenUpdt);
            }
            else
            {
                if (!string.IsNullOrEmpty(resultContent) && resultContent.Contains("error_description"))
                {
                    var tokenParse = new TokenParse(resultContent);
                    Message.Set(ExecutionContext, tokenParse.error_description);
                }
                else
                {
                    Message.Set(ExecutionContext, "Requisição não retornou um token válido.");
                }
            }
        }

        /// <summary>
        /// Valida campos obrigatorios no cadastro para obter token
        /// </summary>
        /// <param name="tokenConfigRecord"></param>
        private void ValidarCamposObrigatorios(Entity tokenConfigRecord)
        {
            if (!tokenConfigRecord.Contains(token_endpoint_attribute))
                throw new ApplicationException($"Entidade de configuração do token não possui campo {token_endpoint_attribute} cadastrado.");
            if (!tokenConfigRecord.Contains(clientid_attribute))
                throw new ApplicationException($"Entidade de configuração do token não possui campo {clientid_attribute} cadastrado.");
            if (!tokenConfigRecord.Contains(client_secret_attribute))
                throw new ApplicationException($"Entidade de configuração do token não possui campo {client_secret_attribute} cadastrado.");
            if (!tokenConfigRecord.Contains(resource_attribute))
                throw new ApplicationException($"Entidade de configuração do token não possui campo {resource_attribute} cadastrado.");

        }
    }

}
