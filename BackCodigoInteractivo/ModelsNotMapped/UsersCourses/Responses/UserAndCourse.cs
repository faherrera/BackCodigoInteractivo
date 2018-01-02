using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Responses
{
    public class UserAndCourse
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();
        public UserAndCourse() { }

        public UserAndCourse(string username, int courseCode)
        {
            this.user = ctx.Users.Where(u => u.Username == username).FirstOrDefault();
            this.course = ctx.Courses.FirstOrDefault(c => c.Code == courseCode);
        }

        public User user { get; set; }
        public Course course { get; set; }
    }
}