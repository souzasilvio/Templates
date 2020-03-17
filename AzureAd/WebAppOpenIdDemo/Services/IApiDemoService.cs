using AzureAd.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppOpenIdDemo.Models;

namespace WebAppOpenIdDemo.Services
{
    public interface IApiDemoService
    {
        Task<List<ClienteModel>> ListarClientes(string accessToken);
    }
}