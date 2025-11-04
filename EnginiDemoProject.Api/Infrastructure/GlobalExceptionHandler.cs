using EnginiDemoProject.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EnginiDemoProject.Api.Infrastructure
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            object errorResponse;

            switch (exception)
            {
                case NotFoundException notFoundEx:
                    statusCode = StatusCodes.Status404NotFound;
                    errorResponse = new
                    {
                        Status = statusCode,
                        Message = notFoundEx.Message
                    };
                    break;
                default:
                    _logger.LogError(exception, "An unhandled exception has occurred: {Message}", exception.Message);
                    statusCode = StatusCodes.Status500InternalServerError;
                    errorResponse = new
                    {
                        Status = statusCode,
                        Message = "An unexpected internal server error occurred."
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }

    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
