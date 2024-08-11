using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReimbursementPoC.Administration.Domain.Common;
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

            // ToDo: add logger
            Console.WriteLine(exception.Message);

            if (exception is BusinessRuleValidationException || exception.GetType().Name == typeof(ValidationException).Name)
            {
                SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            }
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
