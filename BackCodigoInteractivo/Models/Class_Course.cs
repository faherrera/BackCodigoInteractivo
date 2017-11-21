using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Models
{
    public class Class_Course
    {
        [Key]
        public int Class_CourseID { get; set; }
        public int CodeClass { get; set; }
        public String TitleClass { get; set; }
        public String PathVideo { get; set; }
        public String Description { get; set; }
        public int CourseID { get; set; }  

    }
}