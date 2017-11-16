using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Models
{
    public class Resource_class
    {
        [Key]
        public int Resource_ClassID { get; set; }
        public int CodeResource { get; set; }
        public String TitleResource { get; set; }
        public String Description { get; set; }
        public String ExternalLink { get; set; }
       // public String PathResource { get; set; }
        public int Class_CourseID { get; set; }     //The ClassCourse. 

    }
}