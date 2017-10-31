using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using static System.Net.WebRequestMethods;

namespace BackCodigoInteractivo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        AuthRepository _authRepo;
        CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public AuthController()
        {
            _authRepo = new AuthRepository();
        }

        // GET: api/Auth
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auth
        public IHttpActionResult Post(TokenReceived _tr)
        {
            
            

            if (_tr != null)
            {
                if (_authRepo.existsToken(_tr.Token))   //Si viene por body.
                {
                    return Json(_authRepo.returnUserView(_tr.Token));   //Retorno el objeto de tipo UserResponse serializado.
                }

                return NotFound();
            }

            if (Request.Headers.Count() > 0)    //Existen parametros via header.
            {
                string _token = Request.Headers.GetValues("Token").FirstOrDefault();  //Extraigo el token.

                if (_authRepo.existsToken(_token)) //Consulto si es que existe el token.
                {
                    return Json(_authRepo.returnUserView(_token));   //Retorno el objeto de tipo UserResponse serializado.

                }

                return NotFound();

            }

            return BadRequest("ERROR en la peticion, 404");
        }

        // PUT: api/Auth/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Auth/5
        public void Delete(int id)
        {
        }
    }
}
