using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServicoName.API.Utils
{
    public static class ActionDescriptorExtensions
    {
        public static ApiVersionModel GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
                .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
                .Select(kvp => kvp.Value as ApiVersionModel)
                .FirstOrDefault();
        }
    }

    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            var actionApiVersionModel = context.ApiDescription.ActionDescriptor?.GetApiVersion();
            if (actionApiVersionModel == null)
            {
                return;
            }

            var documentVersion = context.ApiDescription.ActionDescriptor.Properties["docName"]
                .ToString()
                .Replace("v", string.Empty);

            var versionParameter = (operation.Parameters.FirstOrDefault(
                p => p.Name == "version"
            ) as NonBodyParameter);
            if (versionParameter != null)
            {   
                versionParameter.Default = documentVersion;
                versionParameter.Enum = new List<object>() { documentVersion };
            }
        }
    }
}