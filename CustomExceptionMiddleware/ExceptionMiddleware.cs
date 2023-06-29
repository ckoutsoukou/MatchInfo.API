using MatchInfo.API.GlobalErrorHandling.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
using NLog.LayoutRenderers;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace MatchInfo.API.CustomExceptionMiddleware
{
    /// <summary>
    /// A class for ExceptionMiddleware.
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// The _next.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Ctor for ExceptionMiddleware.
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes method
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handle exceptions
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDetails = new ErrorDetails();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is KeyNotFoundException)
            {
                errorDetails.StatusCode = (int)HttpStatusCode.NotFound;
                errorDetails.Message = exception.Message;
            }
            else if ((exception is DuplicateNameException) || (exception is ArgumentException) || (exception is ArgumentNullException))
            {
                errorDetails.StatusCode = (int)HttpStatusCode.NotAcceptable;
                errorDetails.Message = exception.Message;
            }

            else if (exception is DbUpdateException)
            {
                errorDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                errorDetails.Message = exception.InnerException.Message;
            }
            else 
            {
                errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorDetails.Message = exception.Message;
            }

            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
