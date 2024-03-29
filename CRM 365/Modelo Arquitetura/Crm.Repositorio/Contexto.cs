﻿using System;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.Net;

namespace Crm.Repositorio
{
    /// <summary>
    /// Objective: This class conect to CRM organization using Microsoft.Xrm.Tooling.Connector.
    /// Autor: Silvio Jose de Souza
    /// </summary>
    public class Contexto
    {
        private static OrganizationServiceProxy _serviceProxy;
        private static object syncRoot = new Object();

        public static OrganizationServiceProxy Proxy
        {
            get
            {
                if (_serviceProxy == null)
                {
                    lock (syncRoot)
                    {
                        var ct = ConfigurationManager.ConnectionStrings["Crm365"].ConnectionString;
                        var conn = new CrmServiceClient(ct);
                        if (!conn.IsReady)
                        {
                            throw new ApplicationException(conn.LastCrmError);
                        }
                        _serviceProxy = conn.OrganizationServiceProxy;
                        _serviceProxy.EnableProxyTypes();
                    }
                }
                return _serviceProxy;
            }
        }

    }
}