using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.CoursesModels.Responses
{
    public class CoursesResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int codeState { get; set; }
        public ICollection<Course> courses { get; set; }
    }
}