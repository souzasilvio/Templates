using AzureAd.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAppOpenIdDemo.Models;

namespace WebAppOpenIdDemo.Services
{
    public class ApiDemoService : IApiDemoService
    {
        private readonly HttpClient httpClient;

        public ApiDemoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:44318/");
        }

        public async Task<List<ClienteModel>> ListarClientes(string accessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync($"api/cliente");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<ClienteModel>>(content);
                return lista;
            }

            throw new
                HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

    }

}