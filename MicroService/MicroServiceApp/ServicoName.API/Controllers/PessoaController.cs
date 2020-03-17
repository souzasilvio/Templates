using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicoName.API.Utils;
using ServicoName.Shared.Utils;
using ServicoName.Shared.Recursos;
using ServicoName.Shared.Models;
using ServicoName.API.Models;

namespace ServicoName.API.Controllers
{
    [ApiVersion("1.0")]
    [Route(Constantes.RoutePrefix)]
    [ApiController]
    public class PessoaController : BaseController
    {

        private readonly IMediator _mediator;

        public PessoaController(IMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            _mediator = mediator;
        }


        [HttpGet]
        [Route("pessoa/{id}")]
        public async Task<IActionResult> PessoaDetails(int id)
        {
            PessoaViewModel model = await _mediator.SendAsync(new UserQuery { Id = userId });

            if (model == null)
                return NotFound();

            return View(model);
        }
    }
}
