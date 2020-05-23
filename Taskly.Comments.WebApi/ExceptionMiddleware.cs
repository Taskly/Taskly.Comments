using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Taskly.Comments.Model.Exceptions;

namespace Taskly.Comments.WebApi
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            var error = new ApiError();
            switch (exception)
            {
                case InvalidArgumentDomainException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    error.Message = ex.Message;
                    break;

                case NotFoundDomainException ex:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    error.Message = ex.Message;
                    break;

                default:
                    _logger.LogError(exception.ToString());
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    error.Message = "Unknown internal error.";
                    break;
            }

            string responseBody = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(responseBody);
        }

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
    }
}
