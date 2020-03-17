using ServicoName.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicoName.API.Aggregators
{
    public class PessoaQuery : IAsyncRequest<PessoaViewModel>
    {
        public int Id { get; set; }
    }
}
