using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Users.ModelFactory
{
    public class MyCourse
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public int Code { get; set; }
        public string Name { get; set; }

        public List<MyCourse> getAllCourses(int userID)
        {
            MyCourse myCourse = new MyCourse();

            List<MyCourse> list = new List<MyCourse>();
            var listFromUser = ctx.UsersCourses;

            foreach (var item in listFromUser)
            {
                if (item.UserID == userID)
                {
                    var curso = ctx.Courses.FirstOrDefault(x => x.Code == item.CourseID);
                    myCourse.Name = curso.Name;
                    myCourse.Code = curso.Code;
                    list.Add(myCourse);
                }
            }

            return list;
        }
    }

  
}