using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Text;
using WorkoutGlobal.UI.Models.ErrorModels;

namespace WorkoutGlobal.UI.Filters.ActionFilters
{
    /// <summary>
    /// Attribute for validate incoming model on UI.
    /// </summary>
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Actions after action method execution.
        /// </summary>
        /// <param name="context">Executed context.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// Actions before action method execution.
        /// </summary>
        /// <param name="context">Executed context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dtoParam = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("ViewModel")).Value;

            if (dtoParam == null)
                context.Result = new BadRequestObjectResult(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Incoming view model in null.",
                    Details = new StackTrace().ToString()
                });

            if (!context.ModelState.IsValid)
            {
                var errorMessage = new StringBuilder();

                foreach (var error in context.ModelState.Values)
                    error.Errors.Select(x => x.ErrorMessage).ToList().ForEach((message) =>
                    {
                        errorMessage.Append(message + " ");
                    });

                context.Result = new BadRequestObjectResult(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "View model isn't valid.",
                    Details = errorMessage.ToString()
                });
            }
        }
    }
}
