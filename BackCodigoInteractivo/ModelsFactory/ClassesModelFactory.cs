using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsFactory
{
    public class ClassesModelFactory
    {
        
        public Class_Course Create (Class_Course _class)
        {
            return new Class_Course()
            {
                Class_CourseID = _class.Class_CourseID,
                CodeClass = _class.CodeClass,
                TitleClass = _class.TitleClass,
                Path_Video = _class.Path_Video,
                CourseID = _class.CourseID,
               // ResourceClasses = _class.ResourceClasses.Select(r => Create(r)).ToList()

            };
        }

        private Resource_class Create(Resource_class r)
        {
            return new Resource_class()
            {
                Resource_ClassID = r.Resource_ClassID,

            };
        }
    }
}