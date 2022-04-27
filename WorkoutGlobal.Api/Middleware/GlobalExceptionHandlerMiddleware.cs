using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Internal server error on WorkoutGlobal API.",
                    Details = "Ensure that request was correct."
                }.ToString());
            }
        }
    }
}
