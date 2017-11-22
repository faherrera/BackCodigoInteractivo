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
    public class RolesController : ApiController
    {
        private CodigoInteractivoContext db = new CodigoInteractivoContext();
        private RoleRepository _roleRepo = new RoleRepository();

        // GET: api/Roles
        public IHttpActionResult GetRoles()
        {
            return Json(_roleRepo.listRoles());
        }

        // GET: api/Roles/5
        public IHttpActionResult GetRole(int id)
        {
            return Json(_roleRepo.detailRole(id));
        }

        // PUT: api/Roles/5
        public IHttpActionResult PutRole(int id, Role role)
        {
            return Json(_roleRepo.putRole(id,role));
        }

        // POST: api/Roles
        public IHttpActionResult PostRole(Role role)
        {
            return Json(_roleRepo.storeRole(role));
        }

        // DELETE: api/Roles/5
        public IHttpActionResult DeleteRole(int id)
        {
            return Json(_roleRepo.deleteRole(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}