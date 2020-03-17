using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ServicoName.API.Utils
{
    public static class ExtensionMethods
    {
        public static Guid ObterCorrelationId(this IHeaderDictionary headers)
        {
            if (headers.ContainsKey("X-Correlation-ID"))
            {
                return new Guid(headers["X-Correlation-ID"].ToString());
            }
            return Guid.Empty;
        }

        public static string ObterIdUsuario(this IHeaderDictionary headers)
        {
            if (headers.ContainsKey("idUsuario"))
            {
                return headers["idUsuario"].ToString();
            }
            return string.Empty;
        }

        //public static async Task GravarTelemetriaMediator<T>(this IRequest<T> request, IMediator mediator, IScopeContext scopeContext)
        //{
        //    await mediator.Send(new AppInsightsRequest()
        //    {
        //        TipoEvento = ServiceAgent.Utils.Enum.TipoEventoInsights.Trace,
        //        TraceTelemetry = new Microsoft.ApplicationInsights.DataContracts.TraceTelemetry()
        //        {
        //            Message = "mediator-Request:" + request.GetType().ToString(),
        //            Properties = {
        //            { "correlationId", scopeContext.CorrelationId.ToString() },
        //            { "idUsuario", scopeContext.IdUsuario },
        //            { "requestType", request.GetType().ToString() },
        //            { "request", JsonConvert.SerializeObject(request) }
        //        }
        //        }
        //    });
        //}

        //public static async Task GravarTelemetriaMediator<T>(this IRequest<T> request, T response, IMediator mediator, IScopeContext scopeContext)
        //{
        //    await mediator.Send(new AppInsightsRequest()
        //    {
        //        TipoEvento = ServiceAgent.Utils.Enum.TipoEventoInsights.Trace,
        //        TraceTelemetry = new Microsoft.ApplicationInsights.DataContracts.TraceTelemetry()
        //        {
        //            Message = "mediator-Response:" + request.GetType().ToString() + ":" + response.GetType().ToString(),
        //            Properties = {
        //              { "correlationId", scopeContext.CorrelationId.ToString() },
        //            { "idUsuario", scopeContext.IdUsuario },
        //            { "requestType", request.GetType().ToString() },
        //            { "responseType", response.GetType().ToString() },
        //            { "request", JsonConvert.SerializeObject(request) },
        //            { "response", JsonConvert.SerializeObject(response) }
        //            }
        //        }
        //    });
        //}

        ////public static async Task GravarDependenciaMediator<T>(this IRequest<T> request, DateTime dataInicio, Task<T> tarefa, IMediator mediator, IScopeContext scopeContext)
        //{
        //    await mediator.Send(new AppInsightsRequest()
        //    {
        //        TipoEvento = ServiceAgent.Utils.Enum.TipoEventoInsights.Dependencia,
        //        Dependency = new Microsoft.ApplicationInsights.DataContracts.DependencyTelemetry()
        //        {
        //            Type = Constantes.NomeDependenciaMediator,
        //            Name = request.GetType().ToString(),
        //            Data = string.Empty,
        //            Timestamp = dataInicio,
        //            Duration = DateTime.UtcNow - dataInicio,
        //            Success = tarefa.IsCompletedSuccessfully,
        //            ResultCode = tarefa.Status.ToString(),
        //            Properties ={ { "correlationId", scopeContext.CorrelationId.ToString() },
        //                          { "idUsuario", scopeContext.IdUsuario } }
        //        }
        //    });
        //}
    }
}