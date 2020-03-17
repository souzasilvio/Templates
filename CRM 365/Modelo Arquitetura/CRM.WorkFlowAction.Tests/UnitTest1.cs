using System;
using System.Activities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using CRM.Actions;
using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace CRM.WorkFlowAction.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private XrmFakedContext fakedContext;
        private XrmFakedWorkflowContext wfContext;
        public UnitTest1()
        {
            fakedContext = new XrmFakedContext();
            wfContext = fakedContext.GetDefaultWorkflowContext();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private Entity GetRegistroToken()
        {
            var endPoint = "https://login.microsoftonline.com/94b1e746-0c85-4ddd-99b7-1e6ac22c732b/oauth2/token";
            var clientId = "5bad115b-8a84-47dc-bae0-831b2b5c90e6";
            var clientSecret = "kzzqngba7/*r*D3zlqZwLNnwyH_3nL7*";
            var resource = "ef66c957-24fc-4a62-9f19-864b23e502de";

            Guid id = new Guid("5bad115b-8a84-47dc-bae0-831b2b5c9999");
            var registro = new Entity("sad_token") { Id = id };
            registro["sad_token_endpoint"] = endPoint;
            registro["sad_clientid"] = clientId;
            registro["sad_client_secret"] = clientSecret;
            registro["sad_resource"] = resource;
            return registro;
        }

        [TestMethod]
        public void ActionGetToken()
        {
            var registro = GetRegistroToken();            
            fakedContext.Initialize(new List<Entity>() { registro });                   
            var inputs = new Dictionary<string, Object> { {"TokenConfig", registro.ToEntityReference() } };                     
            var codeActivity = new JwtTokenAction();
            var result1 = fakedContext.ExecuteCodeActivity<JwtTokenAction>(wfContext, inputs, codeActivity);
            Assert.IsTrue((bool)result1["Sucess"], (string)result1["Message"]);
        }

        [TestMethod]
        public void ActionGetTokenDoCache()
        {
            var registro = GetRegistroToken();
            registro["sad_data_expiracao"] = DateTime.Now;
            registro["sad_valor_token"] = "xxxxxxxxxxxxx";

            fakedContext.Initialize(new List<Entity>() { registro });
            var inputs = new Dictionary<string, Object> { { "TokenConfig", registro.ToEntityReference() } };
            var codeActivity = new JwtTokenAction();
            var result1 = fakedContext.ExecuteCodeActivity<JwtTokenAction>(wfContext, inputs, codeActivity);
            Assert.IsTrue((bool)result1["Sucess"], (string)result1["Message"]);
        }


    }
}
