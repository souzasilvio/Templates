using System;
using System.Activities;
using System.Collections.Generic;
using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace CRM.WorkFlowAction.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
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

        [TestMethod]
        public void TestMethod1()
        {

            var fakedContext = new XrmFakedContext();
            var inputs = new Dictionary<string, Object> { {"City", "Hyderabad" } };
            var result = fakedContext.ExecuteCodeActivity<CRM.Actions.JwtTokenAction>(inputs, null);

        }
    }
}
