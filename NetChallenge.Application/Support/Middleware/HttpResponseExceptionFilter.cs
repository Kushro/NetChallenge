using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetChallenge.Domain.Exceptions;

namespace NetChallenge.Application.Support.Middleware
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessException exception)
            {
                var result = new
                {
                    Title = "Business Exception",
                    Message = exception.Message
                };
                
                context.Result = new ObjectResult(result)
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is Exception ex)
            {
                var result = new
                {
                    Title = "Internal Server Error",
                    Message = ex.Message
                };
                
                context.Result = new ObjectResult(result)
                {
                    StatusCode = 500,
                };
                
                context.ExceptionHandled = true;
            }
        }
    }
}