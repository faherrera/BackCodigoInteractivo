using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Request;
using BackCodigoInteractivo.Repositories.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories.UserCourse
{
    public class ValidationUserCourseRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public bool existUserAndCourse(LinkUserCourseRequest request)
        {
            UserRepository userRepo = new UserRepository();
            ValidationCourseRepository validationCourseRepo = new ValidationCourseRepository();

            if (!userRepo.busyUsername(request.Username))
            {
                return false;
            }

            if (!validationCourseRepo.busyCourseCode(request.CourseCode))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Saber si ya está inscripto en el curso
        /// </summary>
        /// <returns>Bool</returns>
        public bool alreadyEnroled(int UserID, int CourseCode)
        {
            return ctx.UsersCourses.Any(x => x.CourseID == CourseCode && x.UserID == UserID );
        }
    }
}