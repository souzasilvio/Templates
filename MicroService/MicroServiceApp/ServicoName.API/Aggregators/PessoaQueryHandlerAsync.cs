using ServicoName.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api
namespace ServicoName.API.Aggregators
{
    public class PessoaQueryHandlerAsync : IAsyncRequestHandler<PessoaQuery, PessoaViewModel>
    {
        public async Task<PessoaViewModel> Handle(PessoaQuery message)
        {
            // Could query a db here and get the columns we need.

            var viewModel = new PessoaViewModel();
            viewModel.Nome = "sgordon";
            viewModel.Email = "Steve@gmail.com";

            return viewModel;
        }
    }
}
