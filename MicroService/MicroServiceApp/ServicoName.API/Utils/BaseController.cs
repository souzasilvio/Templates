using System.Threading.Tasks;
using ServicoName.Shared.Recursos;
using ServicoName.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace ServicoName.API.Utils
{
    public class BaseController : Controller
    {
        protected  IMediator Mediator { get; }
        protected IStringLocalizer<I18n> Dicionario { get; }
        protected IScopeContext ScopeContext { get; }
       // protected TelemetryClient clienteTelemetria { get; }

        protected BaseController(IMediator mediator, IStringLocalizer<I18n> dicionario, IScopeContext scopeContext)
        {
            Mediator = mediator;
            Dicionario = dicionario;
            ScopeContext = scopeContext;
           // this.clienteTelemetria = clienteTelemetria;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            InicializarScopeContext();
        }


        protected async Task<T> Send<T>(IRequest<T> request)
        {
            //if (request.GetType() != typeof(AppInsightsRequest))
            //    _ = request.GravarTelemetriaMediator(Mediator, ScopeContext);

            var response = await Mediator.Send(request);

            //if (request.GetType() != typeof(AppInsightsRequest))
            //    _ = request.GravarTelemetriaMediator(response, Mediator, ScopeContext);

            return response;
        }

        private void InicializarScopeContext()
        {
            ScopeContext.CorrelationId = Request.Headers.ObterCorrelationId();
            ScopeContext.IdUsuario = Request.Headers.ObterIdUsuario();
        }

    }
}