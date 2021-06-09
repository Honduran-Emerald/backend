using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Emerald.Application.Infrastructure.OperationFilter
{
    public class SyncTokenOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Sync-Token",
                In = ParameterLocation.Header,
                Description = "Synchronization Token",
                Required = false
            });
        }
    }
}
