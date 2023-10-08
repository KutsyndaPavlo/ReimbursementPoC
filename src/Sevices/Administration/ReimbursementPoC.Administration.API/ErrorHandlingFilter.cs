using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Product;
using ReimbursementPoC.Administration.Domain.Program.Rules;
using ReimbursementPoC.Administration.Domain.Service.Exeption;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ReimbursementPoC.Administration.API
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private static void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            //if (context.RouteData.Values["controller"].ToString() == "Proposal")
            //{
            //    if (exception is ProductNotFoundException)
            //        SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            //    else if (exception is SellerNotFoundException)
            //        SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            //    else if (exception is BusinessRuleValidationException)
            //        SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            //}
            if (exception is BusinessRuleValidationException || exception.GetType().Name == typeof(ValidationException).Name)
                SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            else if (exception is ServiceNotFoundException)
                SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            else if (exception is ServiceNotFoundException)
                SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            else if (exception is ProgramCanNotBeDeletedException)
                SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            else if (exception is ServiceCanNotBeDeletedException)
                SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            //else if (exception is ProductNotFoundException)
            //    SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            //else if (exception is SellerNotFoundException)
            //    SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            //else if (exception is ProposalNotFoundException)
            //    SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            //else if (exception is ProductConcurrentUpdateException)
            //    SetExceptionResult(context, exception, HttpStatusCode.Conflict);
            else
            {
                context.Result = new JsonResult($"Something went wrong. Details: {context.Exception}")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        private static void SetExceptionResult(
            ExceptionContext context,
            Exception exception,
            HttpStatusCode code)
        {
            context.Result = new JsonResult(exception.Message)
            {
                StatusCode = (int)code
            };
        }
    }
}
