using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BackCodigoInteractivo.Filters
{
    public class CIAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            
            var authHeader = actionContext.Request.Headers.Contains("Token");  //can get at Authorization header here but no HTTPActionContext in controller


            if (!authHeader)
            {
                throw new HttpResponseException(actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"Este es el mensaje de errr desde actionResult"));
            }

            string TokenReceive = actionContext.Request.Headers.GetValues("Token").FirstOrDefault();

            Debug.WriteLine("===========================================");
            Debug.WriteLine(string.Format("El Token es -> {0}", TokenReceive));
            Debug.WriteLine("===========================================");

        }


    }

}