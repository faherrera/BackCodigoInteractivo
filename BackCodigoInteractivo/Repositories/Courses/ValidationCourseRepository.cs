using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories.Courses
{
    public class ValidationCourseRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        //Exist

        public bool busyCourseCode(int code)
        {
            return ctx.Courses.Any(c=> c.Code == code);
        }
    }
}