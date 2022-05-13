using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Refit;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text;
using WorkoutGlobal.UI.Models.ErrorModels;

namespace WorkoutGlobal.UI.Middlewares
{
    /// <summary>
    /// Middleware for handle exceptions.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Ctor for global exception middleware.
        /// </summary>
        /// <param name="next">Request delegate instance.</param>
        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke some code of actual middleware and go to next middleware.
        /// </summary>
        /// <param name="httpContext">Http context of request/response.</param>
        /// <returns>A task that represents asynchronous operation of going on middleware pipeline.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case ValidationException:
                    case ArgumentNullException:
                        await HandleExceptionAsync(httpContext, exception, StatusCodes.Status400BadRequest);
                        break;
                    case ApiException:
                    case Exception:
                    default:
                        await HandleExceptionAsync(httpContext, exception, StatusCodes.Status500InternalServerError);
                        break;
                }
            }
        }

        /// <summary>
        /// Handle exception.
        /// </summary>
        /// <param name="httpContext">Http context.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="statusCode">Exception status code.</param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(
            HttpContext httpContext,
            Exception exception,
            int statusCode)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            var allMessageText = exception.ToString();

            await response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails()
            {
                StatusCode = statusCode,
                Message = allMessageText,
                Details =  new StackTrace().ToString()
            })).ConfigureAwait(false);
        }
    }
}
