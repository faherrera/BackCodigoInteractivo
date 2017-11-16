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

namespace BackCodigoInteractivo.Controllers
{
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
        [ResponseType(typeof(Resource_class))]
        public IHttpActionResult GetResource_class(int id)
        {
            Resource_class resource_class = db.Resources.Find(id);
            if (resource_class == null)
            {
                return NotFound();
            }

            return Ok(resource_class);
        }

        // PUT: api/ResClass/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResource_class(int id, Resource_class resource_class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resource_class.Resource_ClassID)
            {
                return BadRequest();
            }

            db.Entry(resource_class).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Resource_classExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ResClass
        [ResponseType(typeof(Resource_class))]
        public IHttpActionResult PostResource_class(Resource_class resource_class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resources.Add(resource_class);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resource_class.Resource_ClassID }, resource_class);
        }

        // DELETE: api/ResClass/5
        [ResponseType(typeof(Resource_class))]
        public IHttpActionResult DeleteResource_class(int id)
        {
            Resource_class resource_class = db.Resources.Find(id);
            if (resource_class == null)
            {
                return NotFound();
            }

            db.Resources.Remove(resource_class);
            db.SaveChanges();

            return Ok(resource_class);
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