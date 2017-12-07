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
        public void Post(TokenReceived _tr)
        {
            
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
