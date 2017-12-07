using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsFactory;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserRegisterController : ApiController
    {
        UserRepository ur = new UserRepository();
        CodigoInteractivoContext db = new CodigoInteractivoContext();

     
        // GET: api/User
        public IHttpActionResult Get()
        {
            return Json(ur.listUsers());

        }

        // GET: api/User/5
        public IHttpActionResult  Get(int id)
        {
            return Json(ur.detailUser(id));

        }

        // POST: api/User
        public IHttpActionResult Post(User user)
        {
            return Json(ur.storeUser(user));            
        }

        // PUT: api/User/5
        public IHttpActionResult Put(int id, User user)
        {
            return Json(ur.putUser(id,user));

        }


        // DELETE: api/User/5
        public IHttpActionResult Delete(int id)
        {
            return Json(ur.deleteUser(id));
        }
    }
}
