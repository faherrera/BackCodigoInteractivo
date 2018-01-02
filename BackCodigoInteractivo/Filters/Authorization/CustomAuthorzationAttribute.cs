using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.General;
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
    public class CustomAuthorzationAttribute : AuthorizationFilterAttribute
    {
        protected CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        private const string nameOfAuthorization = "Token";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token;
            bool contain = actionContext.Request.Headers.Authorization != null ? true : false;

            if (!contain) throw new HttpResponseException(actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"No está autorizado para esto"));

            token = actionContext.Request.Headers.Authorization.Parameter;

            if (string.IsNullOrEmpty(token)) throw new HttpResponseException(actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado para esto"));

            var authResponse = haveToken(token);

            if (!authResponse.status) throw new HttpResponseException(actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No está autorizado para esto"));
        }



        public AuthenticationResponse haveToken(string Token)
        {
            AuthenticationResponse authResponse = new AuthenticationResponse();


            if (string.IsNullOrEmpty(Token)) return authResponse; //Devuelvo False si está vacio.

            if (!AlreadyExistForToken(Token)) return authResponse; //Devuelvo False si no existe el token



            return authResponse = new AuthenticationResponse(true, Token);    //Devuelvo true y sigo la petición.

        }

        public bool AlreadyExistForToken(string Token)
        {

            return ctx.Users.Any(x => x.Token == Token);

        }
    }


}