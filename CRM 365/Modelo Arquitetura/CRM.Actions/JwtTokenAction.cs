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
        /// </summary>
        private const string token_endpoint_attribute = "sad_token_endpoint";

        /// <summary>
        /// name of attibute used to input ClientId
        /// </summary>
        private const string clientid_attribute = "sad_clientid";

        /// <summary>
        /// name of attibute used to input client secret
        /// </summary>
        private const string client_secret_attribute = "sad_client_secret";

        /// <summary>
        /// name of attibute used to input resource
        /// </summary>
        private const string resource_attribute = "sad_resource";

        //static string authority = "https://login.microsoftonline.com/sadinformatica.onmicrosoft.com";
        //static string clientId = "94854211-4055-47e9-b896-291967920a57";
        //static string clientSecret = "/rNw1pcpuDZ3IWfXKzSlvY0leSZaZV+nOu8Yx4b8HkI=";
        //static string resource = "https://sadinformatica.onmicrosoft.com/CRM.WebApiApp";

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
        public OutArgument<int> ExpiresIn { get; set; }

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
            var client = new HttpClient();
            Trace("Start....");            
            try
            {
                //get de record used to configure token creation
                EntityReference configRefence = TokenConfig.Get(ExecutionContext);
                var tokenConfigRecord = Service.Retrieve(configRefence.LogicalName, configRefence.Id, new ColumnSet(true));

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
                    ExpiresIn.Set(ExecutionContext, tokenParse.expires_in);
                    Sucess.Set(ExecutionContext, true);
                    Message.Set(ExecutionContext, "Sucess");
                }
                else
                {
                    Message.Set(ExecutionContext, "Requisição não retornou um token válido.");
                }
            }
            catch (Exception ex)
            {
                Message.Set(ExecutionContext, ex.Message);
            }
        }


    }

}
