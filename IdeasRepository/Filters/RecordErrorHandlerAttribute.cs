using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeasRepository.Filters
{
    public class RecordErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                String message = "Error has occured during database request.";
                if(request.HttpMethod.Equals("GET")) {
                    message += "Can't load Data";
                }
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        errorMessage = message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}