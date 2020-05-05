using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Taskly.Comments.WebApi
{
    internal static class SwaggerConfiguration
    {
        internal static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "Comments API",
                    Version = "v1.0",
                    Description = "Taskly Comments API"
                });

                options.CustomSchemaIds(SchemaIdStrategy);
            });
        }

        internal static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(c => { c.RouteTemplate = "/api-docs/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/swagger/v1.0/swagger.json", "Comments API v1.0");
            });
        }

        private static string SchemaIdStrategy(Type currentClass)
        {
            string className = currentClass.Name;
            if (className.EndsWith("Dto"))
            {
                className = className.Remove(className.Length - 3, 3);
            }

            return className;
        }
    }
}
