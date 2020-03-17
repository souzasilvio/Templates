using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AzureAd.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using WebAppOpenIdDemo.Models;
using WebAppOpenIdDemo.Services;

namespace WebAppOpenIdDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly ILogger<HomeController> _logger;
        private readonly IApiDemoService _apiservice;
        public HomeController(ILogger<HomeController> logger, IApiDemoService apiservice,   ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _apiservice = apiservice;
            _tokenAcquisition = tokenAcquisition;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeVendas]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Policy = "RequireElevatedRights")]       
        public IActionResult Contato()
        {
            return View();
        }

        //[Authorize(Roles = "Consulta.Clientes")]
        //[AuthorizeClientRole]
        public IActionResult Clientes()
        {
            string[] scopes = new string[] {
            "https://sadinformatica.com.br/WebApiDemo/user_impersonation",
             "https://sadinformatica.com.br/WebApiDemo/Clientes.Delete",
            "User.Read"};

            var accessToken =
               _tokenAcquisition.GetAccessTokenForUserAsync(scopes).GetAwaiter().GetResult();

            var lista = _apiservice.ListarClientes(accessToken).GetAwaiter().GetResult();
            return View(lista);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
