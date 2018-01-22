using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.ModelsFactory.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.UsersCourses.ModelFactory
{
   

    public class UserCourseModelFactory
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public UserCourseModelFactory() { }

        public UserCourseModelFactory(int UserCourseID,int UserID,int CourseID,bool Access,string pathCertificate, bool isInstructor) {
            this.UserCourseID = UserCourseID;
            this.Username = ctx.Users.Find(UserID).Username;
            this.Course = new SimpleCoursesModelFactory(CourseID);  //Traigo el curso con el model factory.
            this.Access = Access;
            this.pathCertificate = pathCertificate;
            this.isInstructor = isInstructor;

        }

        public int UserCourseID { get; set; }
        public string Username { get; set; }
        public SimpleCoursesModelFactory Course { get; set; }
        public bool Access { get; set; }
        public string pathCertificate { get; set; } 
        public bool isInstructor { get; set; }

    }
}