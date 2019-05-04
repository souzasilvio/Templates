using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using AzureADWebClient.Cache;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using AzureAdDemo.Models;
using Newtonsoft.Json;
using AzureADWebClient.Extensions;

namespace AzureADWebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private static readonly HttpClient Client = new HttpClient();
        private readonly ITokenCacheFactory _tokenCacheFactory;
        private readonly AzureAdOptions _authOptions;

        public HomeController(ITokenCacheFactory tokenCacheFactory, IOptions<AzureAdOptions> authOptions)
        {
            _tokenCacheFactory = tokenCacheFactory;
            _authOptions = authOptions.Value;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            string authority = _authOptions.Instance + _authOptions.TenantId;
            string resource = _authOptions.Resource;
            var cache = _tokenCacheFactory.CreateForUser(User);
            var authContext = new AuthenticationContext(authority, cache);
            
            //App's credentials may be needed if access tokens need to be refreshed with a refresh token
            string clientId = _authOptions.ClientId;
            string clientSecret = _authOptions.ClientSecret;
            var credential = new ClientCredential(clientId, clientSecret);
            var userId = User.GetObjectId();
            var result = await authContext.AcquireTokenSilentAsync(resource, credential,
                new UserIdentifier(userId, UserIdentifierType.UniqueId));

            return result.AccessToken;
        }

        private async Task<HttpResponseMessage> QueryWebApiAsync(string webApiUrl, string relativeUrl)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, webApiUrl + relativeUrl);
            string accessToken = await GetAccessTokenAsync();
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await Client.SendAsync(req);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObterEndereco()
        {
            HttpResponseMessage res = await QueryWebApiAsync(_authOptions.WebApiUrl, "/api/endereco/30570350");
            string rawResponse = await res.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<EnderecoModel>(rawResponse);
            return View(endereco);
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

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
