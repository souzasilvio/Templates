using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Endereco> Get(string id)
        {
            return new Endereco() {
                Cep = id,
                Logradouro = "Rua teste"
            };
        }
    }
}
