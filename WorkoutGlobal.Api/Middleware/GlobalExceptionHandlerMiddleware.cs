using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Middleware
{
    /// <summary>
    /// Custom middleware for handle exceptions globaly.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;

        /// <summary>
        /// Ctor for global exception handler middleware.
        /// </summary>
        /// <param name="next">Request delegate for moving in request pipeline.</param>
        /// <param name="environment">Host enviroment.</param>
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        /// <summary>
        /// Middleware executed code.
        /// </summary>
        /// <param name="httpContext">Http context of request.</param>
        /// <returns>A task that represents asynchronous Invoke operation for executing middleware in pipeline.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                var error = new ErrorDetails();
                switch (exception)
                {
                    case ValidationException:
                        error.StatusCode = StatusCodes.Status400BadRequest;
                        error.Message = "Validation error on WorkoutGlobal API.";
                        error.Details = exception.ToString();
                        break;
                    default:
                        error.StatusCode = StatusCodes.Status500InternalServerError;
                        error.Message = "Internal server error on WorkoutGlobal API.";
                        error.Details = new StackTrace().ToString();
                        break;


                }
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
