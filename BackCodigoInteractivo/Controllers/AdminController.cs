using BackCodigoInteractivo.ModelsNotMapped.Authentication.Login.Request;
using BackCodigoInteractivo.Repositories;
using BackCodigoInteractivo.Repositories.Admin;
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
    public class AdminController : ApiController
    {
        LoginRepository lrepo = new LoginRepository();
        
        [HttpGet]
        public IHttpActionResult Index()
       {
            ///Saber si puede acceder al layout Admin-Dashboard
            ///


            try
            {
                string TOKEN = Request.Headers.GetValues("Token").FirstOrDefault();

                if (!string.IsNullOrEmpty(TOKEN) && AdminRepository.HaveAccess("Administrador", TOKEN))
                {
                    return Ok("Correctamente autorizado.");
                }
                return Unauthorized();
            }
            catch (Exception )
            {

                return Unauthorized();
            }


        }
        public IHttpActionResult LoginAdmin( LoginRequest loginRequest)
          {
            if (loginRequest == null)
            {
                return Content(HttpStatusCode.BadRequest,"No puede estár vacia la peticion");
            }

            var result  = lrepo.processLogin(loginRequest, "Administrador");

            if (result.status)
            {
                return Ok(result.data);
            }

            if (result.codeState == 401)
            {
                return Unauthorized();
            }

            return Content(HttpStatusCode.InternalServerError, "Ocurrio un error, intente nuevamente por favor " + result.message);



        }

    }
}
