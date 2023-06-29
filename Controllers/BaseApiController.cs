using MatchInfo.API.DbContexts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Reflection.Metadata;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MatchInfo.API.Controllers
{
    public class BaseApiController: ControllerBase
    {
        protected readonly UnitOfWork.UnitOfWorkMatchInfo uowMatchInfo;

        /// <summary>
        /// The database context.
        /// </summary>
        protected MatchInfoContext _context => uowMatchInfo?.Context;

        /// <summary>
        /// Automapper instance, used to convert db to dto objects and vice versa
        /// </summary>
        protected IMapper mapper => uowMatchInfo?.Mapper;

        protected ILogger _logger => uowMatchInfo.Logger;

        public BaseApiController(UnitOfWork.UnitOfWorkMatchInfo uow)
        {
            uowMatchInfo = uow;
        }
       
        /// <summary>
        /// Execute a fuction that returns an ActionResult, handle all known exceptions by returning appropriate ActionResult
        /// METHOD WILL NOT BE FIRED, IF ACTIONRESULT IS ALREADY SET
        /// </summary>
        /// <param name="response">The action result to examine and set on action execution and possibly failure</param>
        /// <param name="action">The action to execute</param>
        //protected void RunAction<T>(ref ActionResult<T> response, Func<ActionResult<T>> action)
        //{
        //    //Do not fire the action, if a response has already been set
        //    if (response is null)
        //    {
        //        try
        //        {
        //            //execute the action, and retrieve its response
        //            if (!(action is null))
        //                response = action();
        //        }
        //        //delete failed because id does not exist in the database
        //        catch (KeyNotFoundException ex)
        //        {
        //            _logger.LogError(ex.Message);
        //            response = Problem(statusCode: StatusCodes.Status404NotFound, title: "NOT FOUND", detail: ex.Message);//new NotFoundObjectResult(new { message = "404 Not Found", details = ex.Message });
        //        }
        //        //another record with the same business keys already exists
        //        catch (System.Data.DuplicateNameException ex)
        //        {
        //            _logger.LogError(ex.Message);
        //            response = Problem(statusCode: StatusCodes.Status406NotAcceptable, title:"NOT ACCEPTABLE", detail:ex.Message);//new BadRequestObjectResult(new { message = "400 Bad Request", details = ex.Message }); 
        //        }
        //        //update details are not valid (eg incorrect value for time)
        //        catch (ArgumentException ex)
        //        {
        //            //LogResultValidationError(ex);
        //            //response = ProblemNotAcceptable(ex.Message);
        //        }
        //        //other database error
        //        catch (DbUpdateException ex)
        //        {
        //            //LogResultDbUpdateError(ex);
        //            //response = ProblemBadRequest("DBERROR", ex.InnermostMessage());
        //        }              
        //        //other error
        //        catch (Exception ex)
        //        {
        //            //LogResultExceptionError(ex);
        //            //response = HandleException(ex);
        //        }
        //    }
        //}


        /// <summary>
        /// Check that the controller's method has been called correctly
        /// </summary>
        /// <param name="modelState">The model state of the controller</param>
        /// <param name="body">The parameters sent on the request body</param>
        /// <returns>Non null response on any error, null when input is acceptable</returns>
        //protected void CheckMethodBody<T>(ref ActionResult<T> response, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, object body)
        //{
        //    CheckMethodInputs(ref response, modelState, body);
        //}

        //protected void CheckMethodState<T>(ref ActionResult<T> response, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        //{
        //    if (!modelState.IsValid)
        //    {
        //        var errors = new List<string>();
        //        foreach (var modelStateVal in modelState.Values.Select(d => d.Errors))
        //        {
        //            errors.AddRange(modelStateVal.Select(error =>
        //            !string.IsNullOrWhiteSpace(error.ErrorMessage) ? error.ErrorMessage : error.Exception?.Message
        //            ).Distinct());
        //        }

        //        response = Problem(statusCode: StatusCodes.Status400BadRequest, title: "REQUEST VALIDATION ERRORS", detail: string.Join(Environment.NewLine, errors));               
        //    }
        //}

        //private void CheckMethodInputs<T>(ref ActionResult<T> response, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, object inputs)
        //{

        //    if (inputs is null)
        //    {
        //        response = Problem(statusCode: StatusCodes.Status400BadRequest, title: "REQUEST VALIDATION ERRORS", detail: "Required content has not been specified");//ProblemValidation(errorMessage);
        //    }
        //    else if (!ModelState.IsValid)
        //    {
        //        CheckMethodState(ref response, modelState);
        //    }

        //}
    }
}
