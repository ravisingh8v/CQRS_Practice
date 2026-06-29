using System.ComponentModel.DataAnnotations;
using ET_Common.Responses;

namespace ET_Common.Middleware
{
    public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Unhandled exception occurred while processing {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                NotFoundException ex => (StatusCodes.Status404NotFound, ex.Message),
                UnauthorizedAccessException ex => (StatusCodes.Status401Unauthorized, ex.Message),
                ConflictException ex => (StatusCodes.Status409Conflict, ex.Message),
                BadRequestException ex => (StatusCodes.Status400BadRequest, ex.Message),
                _=> (StatusCodes.Status500InternalServerError, "Internal server error.")
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object?>
            {
                Success = false,
                Error = new ApiError
                {
                    Message = message,
                    StatusCode = statusCode,
                }
            };
            return context.Response.WriteAsJsonAsync(response);
        }

    }
}