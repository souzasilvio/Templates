using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System;

namespace ServicoName.API.Utils
{
    public class AppInsightsInitializer : ITelemetryInitializer
    {
        IHttpContextAccessor httpContextAccessor;

        public AppInsightsInitializer(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var id = httpContextAccessor?.HttpContext?.Request?.Headers?.ObterCorrelationId();
            if (id != null && id != Guid.Empty)
            {
                telemetry.Context.Operation.Id = id.ToString();
            }
        }
    }
}
