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
    public class ClassesCourseController : ApiController
    {
        ClassesRepository cr = new ClassesRepository();

        // GET: api/ClassesCourse
        public IHttpActionResult Get()
        {
            return Json(cr.listClasses());
        }

        // GET: api/ClassesCourse/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ClassesCourse
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ClassesCourse/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ClassesCourse/5
        public void Delete(int id)
        {
        }
    }
}
