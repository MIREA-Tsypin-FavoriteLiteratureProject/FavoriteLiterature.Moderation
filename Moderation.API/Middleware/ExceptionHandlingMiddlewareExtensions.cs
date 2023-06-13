using System.Net;
using System.Security;
using System.Text.Json;

namespace FavoriteLiterature.Moderation.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var details = new HttpValidationProblemDetails();

        switch (exception)
        {
            case ArgumentException ex:
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                details.Title = "One or more validation errors occurred.";
                details.Status = (int) HttpStatusCode.BadRequest;
                details.Errors.Add(ex.ParamName?.ToLower() ?? "Validation Error", new[] {ex.Message});
                break;
            case UnauthorizedAccessException ex:
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
                details.Title = "Authorization Error";
                details.Status = (int) HttpStatusCode.Unauthorized;
                details.Errors.Add("Authorization Error", new[] {ex.Message});
                break;
            case SecurityException ex:
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
                details.Title = "Security Error";
                details.Status = (int) HttpStatusCode.Forbidden;
                details.Errors.Add("Security Error", new[] {ex.Message});
                break;
            case InvalidOperationException ex:
                details.Title = "Invalid Operation Error";
                details.Status = (int) HttpStatusCode.Conflict;
                details.Errors.Add("Invalid Operation Error", new[] {ex.Message});
                break;
            default:
                details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                details.Title = "Internal Server Error";
                details.Status = (int) HttpStatusCode.InternalServerError;
                details.Errors.Add("Internal Server Error", new[] {exception.Message});
                break;
        }

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = details.Status.Value;

        await context.Response.WriteAsync(JsonSerializer.Serialize(details));
    }
}