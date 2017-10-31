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
    public class UserController : ApiController
    {
        UserModelFactory _user;
        UserRepository ur = new UserRepository();
        CodigoInteractivoContext db = new CodigoInteractivoContext();

        public UserController()
        {
            _user = new UserModelFactory();
        }

        // GET: api/User
        public IEnumerable<User> Get()
        {
            return ur.getAllUsers().ToList().Select(u => _user.Create(u));

        }

        // GET: api/User/5
        public IHttpActionResult Get(int id)
        {
            return Json(ur.returnUserJson(id));
        }

        // POST: api/User
        public IHttpActionResult Post(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return Json(ur.returnUserJson(user.UserID));
             
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
