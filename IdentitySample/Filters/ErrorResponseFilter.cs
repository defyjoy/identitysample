using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Filters
{
    public class ErrorResponseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                context.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
                context.Result.ExecuteResult(context.Controller.ControllerContext);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = filterContext.Controller.ViewData,
                    TempData = filterContext.Controller.TempData
                };
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.ExceptionHandled = true;
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
        }
    }
}