using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReimbursementPoC.Program.Domain.Product;
using ReimbursementPoC.Program.Domain.Service.Exeption;
using System.Net;

namespace ReimbursementPoC.Program.API
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

            if (exception is ServiceNotFoundException)
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

            context.Result = new JsonResult($"Something went wrong. Details: {context.Exception}")
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
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
