using BackCodigoInteractivo.ModelsNotMapped.UserAccount.Request;
using BackCodigoInteractivo.Repositories;
<<<<<<< HEAD
=======
using BackCodigoInteractivo.Repositories.Auth;
>>>>>>> testing
using BackCodigoInteractivo.Repositories.UserAccount;
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

    public class UserAccountController : ApiController
    {
        UserCourseRepository repo = new UserCourseRepository();
        UserAccountRepository userAccountRepo = new UserAccountRepository();
<<<<<<< HEAD
        public IHttpActionResult Index()
        {
            return Ok();
=======

        [HttpPost]
        public IHttpActionResult Index()
        {

            try
            {
                string TOKEN = Request.Headers.GetValues("Token").FirstOrDefault();

                if (!string.IsNullOrEmpty(TOKEN) && CredentialsRepository.HaveAccess("Estudiante", TOKEN))
                {
                    return Ok("Correctamente autorizado.");
                }
                return Unauthorized();
            }
            catch (Exception e)
            {

                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
>>>>>>> testing
        }

        /// <summary>
        /// Unsuscribe to Course.
        /// </summary>
        /// <param name="id"></param>
        /// 
        /// <returns></returns>
        /// 

        [HttpPost]
        public IHttpActionResult Unsubscribe(int id)
        {
            try
            {

                string Token = Request.Headers.GetValues("Token").FirstOrDefault();

                if (string.IsNullOrEmpty(Token) || id == 0)
                {
                    return Unauthorized();
                }

                var response = repo.DeleteFromCourseCode(id, Token);

                if (!response.status) return Content((HttpStatusCode)response.codeState, response.message);

                return Ok();

            }
            catch (Exception e)
            {

                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public IHttpActionResult PutAccount(UpdateAccountRequest UpdateAccountRequest)
        {
            try
            {
                string Token = Request.Headers.GetValues("Token").FirstOrDefault();

                if (string.IsNullOrEmpty(Token))
                {
                    return Unauthorized();
                }


                var response = userAccountRepo.UpdateOwnAccount(Token,UpdateAccountRequest);

                if (!response.status)
                {
                    return Content((HttpStatusCode)response.codeState,response.message);
                }

                return Ok(response.data);
            }
            catch (Exception e)
            {

                return Content(HttpStatusCode.InternalServerError,"Ocurrio un error, comunicate con el Admin por favor.");
            }
        }
    }
}
