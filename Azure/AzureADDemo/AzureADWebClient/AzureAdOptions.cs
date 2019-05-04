using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureADWebClient
{


    public class AzureAdOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Instance { get; set; }

        public string Domain { get; set; }

        public string TenantId { get; set; }

        public string CallbackPath { get; set; }

        public string Resource { get; set; }

        public string WebApiUrl { get; set; }

        public string PostLogoutRedirectUri { get; set; }

        public Func<AuthorizationCodeReceivedContext, Task> OnAuthorizationCodeReceived { get; set; }

        public Func<AuthenticationFailedContext, Task> OnAuthenticationFailed { get; set; }
    }
}
