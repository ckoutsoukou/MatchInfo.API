using MatchInfo.API.CustomExceptionMiddleware;
using MatchInfo.API.GlobalErrorHandling.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
//using LoggerService;
using System.Net;

namespace MatchInfo.API.GlobalErrorHandling.Extensions
{
    /// <summary>
    /// A class for ExceptionMiddlewareExtensions.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configures custom exception middleware.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
