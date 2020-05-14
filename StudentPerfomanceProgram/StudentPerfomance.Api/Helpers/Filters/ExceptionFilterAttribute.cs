using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace StudentPerfomance.Api.Helpers.Filters
{
    public class ExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult
            {
                Content = $"OOps! Something went wrong(...",
                StatusCode = 500
            };
        }
    }
}
