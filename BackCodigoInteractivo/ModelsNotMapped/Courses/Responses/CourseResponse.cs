using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsFactory;
using BackCodigoInteractivo.ModelsNotMapped.Courses.ModelFactory;
using BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped
{
    public class CourseResponse : BaseResponse
    {
        public CourseResponse() { }

        public CourseResponse(CourseModelFactory course = null, bool status = false, string message = "Error en la clase", int codeState = 0)
        {
            this.status = status;
            this.message = message;
            this.codeState = codeState;
            data = course;
        }

    }

    public class CourseResponses : BaseResponses
    {
        public CourseResponses(ICollection<CourseModelFactory> course = null, bool status = false, string message = "Error en la clase", int codeState = 0)
        {
            this.status = status;
            this.message = message;
            this.codeState = codeState;
            this.data = course;
        }

        public ICollection<CourseModelFactory> data { get; set; }

    }
}