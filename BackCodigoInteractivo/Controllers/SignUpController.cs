using BackCodigoInteractivo.ModelsNotMapped.Authentication.SignUp.Request;
using BackCodigoInteractivo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BackCodigoInteractivo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE")]
    public class SignUpController : ApiController
    {
        private SignUpRepository sup = new SignUpRepository();
         
        [HttpPost]
        public IHttpActionResult SignUp(SignUpRequest request)
        {
            return Json(sup.processingSignup(request));
        }
    }
}
