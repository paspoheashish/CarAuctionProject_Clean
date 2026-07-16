using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Application exception occurred");

                var problem = new ProblemDetails
                {
                    Title = "Application error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Type = "BadRequest"
                };

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsJsonAsync(problem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");

                var problem = new ProblemDetails
                {
                    Title = "Unexpected error",
                    Detail = "An unexpected error occurred.",
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "InternalServerError"
                };

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }

}

