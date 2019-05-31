using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DockerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient Client = new HttpClient();
        public IConfiguration Configuration { get; }

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
      

        private async Task<HttpResponseMessage> QueryWebApiAsync(string webApiUrl, string relativeUrl)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, webApiUrl + relativeUrl);
            //string accessToken = await GetAccessTokenAsync();
            //req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await Client.SendAsync(req);
        }


        [HttpGet]
        public async Task<IActionResult> Endereco()
        {
            //Api1
            // string apiendereco = "https://WebApi1:443";
            string apiendereco = Configuration["BackEnd:ApiEndereco"];
            HttpResponseMessage res = await QueryWebApiAsync(apiendereco, "/api/endereco/30570350");
            string rawResponse = await res.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<Endereco>(rawResponse);
            return View(endereco);
        }

        [HttpGet]
        public async Task<IActionResult> Cliente()
        {
            string apicliente = Configuration["BackEnd:ApiCliente"];
            try
            {
                
                HttpResponseMessage res = await QueryWebApiAsync(apicliente, "/api/cliente/845.854.476-87");
                string rawResponse = await res.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(rawResponse);
                return View(cliente);
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Url: " + apicliente + "/api/cliente/845.854.476-87 " + ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
