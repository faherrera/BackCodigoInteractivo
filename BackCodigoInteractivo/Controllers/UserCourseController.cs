using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Responses;
using BackCodigoInteractivo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackCodigoInteractivo.Controllers
{
    //[AuthorizationToken]

    public class UserCourseController : ApiController
    {
        UserCourseRepository userCourseRepo = new UserCourseRepository();
        AuthRepository authRepo = new AuthRepository();
        UsersCoursesResponse usersCoursesResponse;
        

        // GET: api/UserCourse
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        /// <summary>
        /// Este metodo retorna todos los cursos a los que pertenece determinado usuario.
        /// Es decir, a todos los UserCourse donde UserID sea igual al del Username solicitado.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        // GET: api/UserCourse/Username
        [HttpGet]
        public IHttpActionResult Get(HttpRequestMessage request,string username)
        {
            return Json(userCourseRepo.ListAllCoursesForEachUser(request,username));
        }
        // GET: api/UserCourse/5
        public IHttpActionResult Get(HttpRequestMessage request,int id)
        {
            return Json(userCourseRepo.detailUserCourse(request, id));
        }

        // POST: api/UserCourse
        public IHttpActionResult Post(HttpRequestMessage request, User_Course userCourse)
        {
            return Json(userCourseRepo.linkUserAndCourse(request,userCourse));
        }

        // PUT: api/UserCourse/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserCourse/5
        public void Delete(int id)
        {
        }
    }
}
