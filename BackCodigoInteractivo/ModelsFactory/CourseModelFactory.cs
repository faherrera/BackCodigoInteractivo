using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsFactory
{
    public class CourseModelFactory
    {
        public Course Create( Course course)
        {
            return new Course()
            {
                CourseID = course.CourseID,
                Code = course.Code,
                Description = course.Description,
                Name = course.Name,
                Duration = course.Duration,
                TypeCourse = course.TypeCourse,
                Mode = course.Mode,
                Level = course.Level,
                Video_preview = course.Video_preview,
                Thumbnail = course.Thumbnail,
                ProfessorID = course.ProfessorID                
            };
        }

        private Class_Course Create(Class_Course _class)
        {
            return new Class_Course()
            {
                Class_CourseID = _class.Class_CourseID,
                CodeClass = _class.CodeClass,
                TitleClass = _class.TitleClass,
                Path_Video = _class.Path_Video,
                CourseID = _class.CourseID
            };
        }
    }
}