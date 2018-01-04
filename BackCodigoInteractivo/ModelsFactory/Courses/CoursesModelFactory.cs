using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsFactory.Courses
{
   
    public class SimpleCoursesModelFactory
    {
      
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();


        public SimpleCoursesModelFactory() { }

        public SimpleCoursesModelFactory(int code)
        {

            var _course = ctx.Courses.FirstOrDefault(x => x.Code == code);


            Code = code;
            Name = _course.Name;
            Description = _course.Description;
            Duration = _course.Duration;
            Mode = Enum.GetName(typeof(Models.ModeEnum),_course.Mode - 1);
            Level = Enum.GetName(typeof(Models.LevelEnum),_course.Level - 1);
            TypeCourse = Enum.GetName(typeof(Models.TypesCourseEnum),_course.TypeCourse - 1);
            Video_preview = _course.Video_preview;
            Thumbnail = _course.Thumbnail;
            ProfessorID = _course.ProfessorID;

        }


        public int Code { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Duration { get; set; }
        public string TypeCourse { get; set; }
        public string Mode { get; set; }
        public string Level { get; set; }
        public string Video_preview { get; set; }
        public string Thumbnail { get; set; }
        public int? ProfessorID { get; set; }       //The professor of the course. 

    }
}