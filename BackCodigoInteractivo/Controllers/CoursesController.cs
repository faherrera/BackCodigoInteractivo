﻿using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsFactory;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.ModelsNotMapped.CoursesModels;
using BackCodigoInteractivo.ModelsNotMapped.CoursesModels.Responses;
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
    public class CoursesController : ApiController
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();
        private CourseRepository cr = new CourseRepository();
        private CourseModelFactory cmf;

        public CoursesController()
        {
            cmf = new CourseModelFactory();
        }
        // GET: api/Courses
        public CoursesResponse Get()
        {
            return cr.listCourses();
        }

        // GET: api/Courses/5
        public IHttpActionResult Get(int id)
        {
            return Json(cr.detailCourse(id));

        }

        // POST: api/Courses
        public IHttpActionResult Post(PCourse _pcourse)
        {
            return Json(cr.storeCourse(_pcourse));
        }

        // PUT: api/Courses/5
        public IHttpActionResult Put(int id, PCourse _pcourse)
        {
            return Json(cr.putCourse(id,_pcourse));
        }

        // DELETE: api/Courses/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return Json(cr.deleteCourse(id));
        }
     }
}