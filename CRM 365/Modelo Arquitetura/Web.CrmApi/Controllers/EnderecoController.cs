using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.CrmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        // POST api/values
        [HttpPost]
        public Endereco Post()
        {
            return new Endereco() {Cep = "30570320", Logradouro = "Rua Teste" };
        }
    }
}
