using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureAd.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;

namespace WebApiDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ClienteModel> Get()
        {
            return ClienteModel.GetTestData();
        }

        [HttpGet("obterporid")]
        
        public ClienteModel GetById([FromQuery] string id)
        {
            ScopesRequiredHttpContextExtensions.VerifyUserHasAnyAcceptedScope(Request.HttpContext,
                "Clientes.Delete");

            return ClienteModel.GetTestDataById(id);
        }
    }
}
