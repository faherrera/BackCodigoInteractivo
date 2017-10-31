using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackCodigoInteractivo.Controllers
{
    public class LoginController : ApiController
    {
        CodigoInteractivoContext cic = new CodigoInteractivoContext();
        CredentialsRepository cr = new CredentialsRepository();

        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            if (cr.existUsername(user.Username)){   //Veo si existe mi usuario.

                if (cr.validationCredentials(user.Username,user.Password)){ //Veo si está validada las credenciales. 

                    string _token = cr.findUserFromUsername(user.Username).Token;   //Token del Usuario.

                    //Devuelvo el token, un mensaje y el acceso.
                    return Json(cr.transformTokenToObject("Acceso Permitido",_token,true));

                }

                // Devuelvo mensaje de que no es correcta la contraseña.
                return Json(cr.transformTokenToObject("Acceso Denegado, contraseña incorrecta", null, false));

            }

            //No existe el usuario.
            
            return Json(cr.transformTokenToObject("Acceso Denegado, no existe el usuario", null, false));

        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
