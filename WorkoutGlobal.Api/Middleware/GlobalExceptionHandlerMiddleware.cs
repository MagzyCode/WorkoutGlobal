using System.Diagnostics;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch
            {
                httpContext.Response.ContentType = "application/json";

                var responce = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error on WorkoutGlobal API.",
                    Details = _environment.IsDevelopment()
                        ? new StackTrace().ToString()
                        : "Ensure that request was correct."
                };

                await httpContext.Response.WriteAsync(responce.ToString());
            }
        }
    }
}
