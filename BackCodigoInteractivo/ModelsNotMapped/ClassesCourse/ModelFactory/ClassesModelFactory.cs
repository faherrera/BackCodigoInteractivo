using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.ClassesCourse.ModelFactory
{
    public class ClassesModelFactory
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public ClassesModelFactory() { }

        public ClassesModelFactory(int code, String title, string path, string external, int codeCourse)
        {
            this.CodeClass = code;
            this.TitleClass = title;
            this.PathVideo = path;
            this.ExternalLink = external;
            this.CourseID = codeCourse;
            this.Course = ctx.Courses.Where(x => x.Code == codeCourse).FirstOrDefault();
            this.Resources = ctx.Resources.Where(x => x.Class_CourseID == code).ToList();
        }

        public int CodeClass { get; set; }
        public String TitleClass { get; set; }
        public String PathVideo { get; set; }
        public String ExternalLink { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public ICollection<Resource_class> Resources { get; set; }
    }
}
