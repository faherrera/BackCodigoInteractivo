using BackCodigoInteractivo.Filters;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Request;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Responses;
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
    public class UserCourseController : ApiController
    {
        UserCourseRepository userCourseRepo = new UserCourseRepository();
        AuthRepository authRepo = new AuthRepository();
        

        // GET: api/UserCourse
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(userCourseRepo.GetAllUserCourses());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Este metodo retorna todos los cursos a los que pertenece determinado usuario.
        /// Es decir, a todos los UserCourse donde UserID sea igual al del Username solicitado.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        // GET: api/UserCourse/?username="someUsername"
        [HttpGet]
        public IHttpActionResult GetFromUsername(string username)
        {

            return Json(userCourseRepo.ListAllCoursesForEachUser(username));
        }

        /// <summary>
        /// Este metodo retorna todos los cursos a los que pertenece determinado usuario según determinados filtros.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        // GET: api/UserCourse/?username="someUsername"
        [HttpGet]
        public IHttpActionResult GetFromUsernameWithBooleanFilters(string username,string filter,string valueFilter= "true")
        {
            return Json(userCourseRepo.ListAllCoursesForEachUser(username,filter,valueFilter));
        }



        // GET: api/UserCourse/5
        public IHttpActionResult GetID(int id)
        {

            return Json(userCourseRepo.detailUserCourse(id));
        }

        // POST: api/UserCourse
        public IHttpActionResult Post(LinkUserCourseRequest userCourseRequest)
        {
           
            return Json(userCourseRepo.linkUserAndCourse(userCourseRequest));
        }

        [HttpPut]
        public IHttpActionResult PutBool(int courseCode, string username, string put)
        {
            var repo = userCourseRepo.UpdateBool(courseCode,username,put);

            if (!repo.status)
            {
                HttpStatusCode statusCode = (HttpStatusCode)repo.codeState;

                return Content(statusCode, repo.message);
            }

            return Ok(repo.message);
        }

        // PUT: api/UserCourse/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserCourse/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var repo = userCourseRepo.DeleteUserCourse(id);

            if (!repo.status)
            {
                HttpStatusCode statusCode = (HttpStatusCode)repo.codeState;

                return Content(statusCode, repo.message);
            }

            return Ok(repo.message);
        }

        [HttpPost]
        public IHttpActionResult BelongToEnrollment(int ClassCode)
        {
            try
            {
                string TOKEN = Request.Headers.GetValues("Token").FirstOrDefault();

                if (string.IsNullOrEmpty(TOKEN) || ClassCode == 0)
                {
                    return Unauthorized();
                }

                if (!userCourseRepo.BeEnrroled(TOKEN, ClassCode))
                {
                    return Unauthorized(); 
                }

                return Ok();

            }
            catch (Exception e)
            {

                return Content(HttpStatusCode.InternalServerError,"Ocurrió un error, "+e.Message);
            }
        }
    }
}
