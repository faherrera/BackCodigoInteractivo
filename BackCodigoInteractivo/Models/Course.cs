using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Models
{
    public enum TypesCourseEnum
    {
        free,premium
    }

    public enum ModeEnum
    {
        presencial,remoto
    }

    public enum LevelEnum
    {
        basico,intermedio,avanzado
    }

    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        public int Code { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Duration { get; set; }
        public TypesCourseEnum TypeCourse { get; set; }
        public ModeEnum Mode { get; set; }
        public LevelEnum Level { get; set; }
        public string Video_preview { get; set; }
        public string Thumbnail { get; set; }
        public int? ProfessorID { get; set; }       //The professor of the course. 

        //public ICollection<User_Course> UserCourse { get; set; }
    }
}