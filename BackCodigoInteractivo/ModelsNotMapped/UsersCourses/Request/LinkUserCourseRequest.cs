using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Request
{
    public class LinkUserCourseRequest
    {
        public LinkUserCourseRequest() { }

        public LinkUserCourseRequest(string username, int courseCode,bool Access,bool isInstructor) {

            this.Username = username;
            this.CourseCode = courseCode;
            this.Access = Access;
            this.isInstructor = isInstructor;
        }

        public string Username { get; set; }
        public int CourseCode { get; set; }
        public bool Access { get; set; }
        public bool isInstructor { get; set; }
    }
}