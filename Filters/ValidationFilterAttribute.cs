using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MatchInfo.API.Filters
{
    /// <summary>
    /// A class for ValidationFilterAttribute
    /// </summary>
    public class ValidationFilterAttribute : IActionFilter
    {
        /// <summary>
        /// Ctor for ValidationFilterAttribute.
        /// </summary>
        public ValidationFilterAttribute() {}

        /// <summary>
        /// Called before excuting a contoller method.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }

        /// <summary>
        /// Called before after a contoller method.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}
