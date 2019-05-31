using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        
        // GET api/values/5
        [HttpGet("{cpf}")]
        public ActionResult<Cliente> Get(string cpf)
        {
            return new Cliente()
           {
                Cpf = cpf,
                Nome = "Marcos Silva"
            };
        }
    }
}
