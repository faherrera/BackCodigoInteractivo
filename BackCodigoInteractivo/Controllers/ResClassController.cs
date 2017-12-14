using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.Repositories;
using System.Web.Http.Cors;

namespace BackCodigoInteractivo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE")]

    public class ResClassController : ApiController
    {
        private ResClassRepository _rcr = new ResClassRepository();
        private CodigoInteractivoContext db = new CodigoInteractivoContext();

        // GET: api/ResClass
        public IHttpActionResult GetResources()
        {
            return Json(_rcr.listResources());
        }

        // GET: api/ResClass/5
        public IHttpActionResult GetResource_class(int id)
        {
            return Json(_rcr.detailResource(id));
        }

        // PUT: api/ResClass/5
        public IHttpActionResult PutResource_class(int id, Resource_class resource_class)
        {
            return Json(_rcr.editResource(id, resource_class));
        }

        // POST: api/ResClass
        public IHttpActionResult PostResource_class(Resource_class resource_class)
        {
            return Json(_rcr.storeResource(resource_class));
        }

        // DELETE: api/ResClass/5
        [ResponseType(typeof(Resource_class))]
        public IHttpActionResult DeleteResource_class(int id)
        {
            return Json(_rcr.deleteResource(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Resource_classExists(int id)
        {
            return db.Resources.Count(e => e.Resource_ClassID == id) > 0;
        }
    }
}