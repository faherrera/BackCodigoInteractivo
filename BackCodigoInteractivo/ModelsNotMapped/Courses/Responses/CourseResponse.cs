using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped
{
    public class CourseResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int codeState { get; set; }
        public Object obj {get;set;}
        public ICollection<Class_Course> _classesCourse { get; set; }
    }
}