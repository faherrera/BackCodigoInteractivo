using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Authentication.General
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse() { }

        public AuthenticationResponse(bool status = false,string token = "") { this.status = status; this.token = token; }

        public bool status { get; set; }
        public string token { get; set; }
    }

    public class UserCourseResponse
    {
        public UserCourseResponse() { }

        public UserCourseResponse(User user, Course course) {
            this.user = user;
            this.course = course;
        }

        public User user { get; set; }
        public Course course { get; set; }
    }
}