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
                String errorMessage = "Error has occured during database request.";
                if(request.HttpMethod.Equals("GET")) {
                    errorMessage += "Can't load Data";
                }
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        ErrorMessage = "Error has occured during database request."
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}