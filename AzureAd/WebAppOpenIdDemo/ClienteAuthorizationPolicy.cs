using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppOpenIdDemo
{
    public static class ClientesAuthorizationPolicy
    {
        public static string Name => "Consulta.Clientes";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.RequireRole("Consulta.Clientes");
    }
}
