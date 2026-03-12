using System.Net;
using System.Text.Json;

namespace AfriStyle.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception for request {Method} {Path}",
                    context.Request.Method, context.Request.Path);

                await WriteErrorResponse(context, ex);
            }
        }

        private static Task WriteErrorResponse(HttpContext context, Exception ex)
        {
            var (statusCode, message) = ex switch
            {
                ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
                InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again.")
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = JsonSerializer.Serialize(new
            {
                error = message,
                statusCode = (int)statusCode,
                timestamp = DateTime.UtcNow
            });

            return context.Response.WriteAsync(errorResponse);
        }
    }
}
