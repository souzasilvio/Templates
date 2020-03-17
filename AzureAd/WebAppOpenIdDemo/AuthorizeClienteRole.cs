using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppOpenIdDemo
{
    public class AuthorizeClientRole : AuthorizeAttribute
    {
        public AuthorizeClientRole() : base(ClientesAuthorizationPolicy.Name)
        {
        }
    }

}
