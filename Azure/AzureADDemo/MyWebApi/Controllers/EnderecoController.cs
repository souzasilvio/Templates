using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureAdDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {

        [HttpGet("{cep}")]
        public ActionResult<EnderecoModel> Get(string cep)
        {
            var endereco = new EnderecoModel()
            {
                Cep = cep,
                Logradouro = "Rua teste",
                Cidade = "Belo Horizonrte"

            };
            return Ok(endereco);
        }

        
    }
}
