using BackCodigoInteractivo.Models;
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
        public IHttpActionResult Get(int id)
        {
            return Json(cr.detailClass(id));
        }

        // POST: api/ClassesCourse
        public IHttpActionResult Post(Class_Course _class)
        {
            return Json(cr.storeClass(_class));
        }

        // PUT: api/ClassesCourse/5
        public IHttpActionResult Put(int id, Class_Course _class)
        {
            return Json(cr.putClass(id,_class));
        }

        // DELETE: api/ClassesCourse/5
        public IHttpActionResult Delete(int id)
        {
            return Json(cr.deleteClass(id));
        }

    }
}
