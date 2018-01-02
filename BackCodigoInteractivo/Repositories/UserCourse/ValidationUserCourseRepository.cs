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
    }
}