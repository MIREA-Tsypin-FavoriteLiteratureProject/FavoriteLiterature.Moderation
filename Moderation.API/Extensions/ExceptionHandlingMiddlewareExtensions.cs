using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteLiterature.Moderation.Extensions;

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder) 
        => builder.UseMiddleware<ExceptionHandlingMiddleware>();
    
    private class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
    
        public async Task Invoke(HttpContext context)
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
    
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case ArgumentException argumentException:
                    problemDetails.Title = "Validation error";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = argumentException.Message;
                    break;
                default:
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.Message;
                    break;
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/problem+json";
        
            var json = JsonSerializer.Serialize(problemDetails);
        
            return context.Response.WriteAsync(json);
        }
    }
}