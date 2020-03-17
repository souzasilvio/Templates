using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppOpenIdDemo
{
    public static class VendasAuthorizationPolicy
    {
        public static string Name => "Sales";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.RequireClaim("groups", "65ef97b1-105a-4d29-9f2a-df429eb253d0");
    }
}
