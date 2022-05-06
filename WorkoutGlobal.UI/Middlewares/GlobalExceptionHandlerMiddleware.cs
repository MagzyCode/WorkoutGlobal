using System.Text;

namespace WorkoutGlobal.UI.Middlewares
{
    /// <summary>
    /// Middleware for handle exceptions.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Sets request delegate.
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
                // TODO: Update this method for existed exceptions.
                var url = new StringBuilder();
                //url.Append($"/Home/Error?message={exception.Message}&");

                //switch (exception)
                //{
                //    case Refit.ValidationApiException:
                //    case Refit.ApiException:
                //        var currentApiException = exception as Refit.ApiException;
                //        url.Append($"statusCode={currentApiException.StatusCode}&");
                //        url.Append($"method={currentApiException.HttpMethod.Method}");
                //        break;
                //    default:
                //        url.Append($"statusCode={(HttpStatusCode)exception.HResult}");
                //        break;
                //}

                httpContext.Response.Redirect(url.ToString());
            }
        }
    }
}
