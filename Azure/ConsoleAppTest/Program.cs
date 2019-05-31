using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

                var configuration = builder.Build();

                string KeyValtName = configuration["KeyValtName"];
                string ClientId = configuration["ClientId"];
                string TenantId = configuration["TenantId"];
                string ClientSecret = configuration["ClientSecret"];
                var util = new Azure.Utils.KeyVaultUtil(KeyValtName, ClientId, ClientSecret, TenantId);

                var secret = util.GetSecret("Chave1").GetAwaiter().GetResult();
                var secret2 = util.GetSecret("Chave1").GetAwaiter().GetResult();
                var secret3 = util.GetSecret("Chave1").GetAwaiter().GetResult();
                Console.WriteLine($"Chave1: {secret}");
                Console.WriteLine($"Chave1: {secret2}");
                Console.WriteLine($"Chave1: {secret3}");

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
