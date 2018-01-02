using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Courses.ModelFactory
{
   

    public class CourseModelFactory
    {
        private String[] modeArray = new String[] { "_", "Presencial", "Remoto" };
        private String[] levelArray = new String[] { "_", "Basico", "Intermedio", "Avanzado" };
        private String[] typeArray = new String[] { "_", "Free", "Premium" };

        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();


        public CourseModelFactory() { }

        public CourseModelFactory(int code, string name,string description,string duration, TypesCourseEnum type, ModeEnum mode, LevelEnum level, string video, string thumb,int? profesorCode)
        {
             

            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.Duration = duration;
            this.Mode = modeArray[(int)mode];
            this.Level = levelArray[(int)level];
            TypeCourse = typeArray[(int)type];
            this.Video_preview = video;
            this.Thumbnail = thumb;
            this.ProfessorID = profesorCode;
            this.Classes = ctx.Classes.Where(x => x.CourseID == code).ToList();
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

        public ICollection<Class_Course> Classes { get; set; }
        //Aquí va a ir la relación con el usuario profesor.

    }
}