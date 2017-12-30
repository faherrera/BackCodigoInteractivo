using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BackCodigoInteractivo.Repositories
{
    public class AuthorizationToken : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;  //can get at Authorization header here but no HTTPActionContext in controller
        }
    }
}