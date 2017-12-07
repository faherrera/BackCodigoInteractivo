﻿using BackCodigoInteractivo.DAL;
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
    public class LoginController : ApiController
    {
        LoginRepository lrepo = new LoginRepository();
    
        //// GET: api/Login
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Login/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Login
        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            return Json(lrepo.processLogin(user));
        }

        //// PUT: api/Login/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Login/5
        //public void Delete(int id)
        //{
        //}
    }
}
