using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.UsersCourses.ModelFactory
{
    public class UserCourseEnrollment
    {
        public int UserCourseID { get; set; }
        public string Username { get; set; }
        public string CourseName { get; set; }
        public bool Access { get; set; }
        public bool IsInstructor { get; set; }
        public int CourseCode { get; set; }
    }
}