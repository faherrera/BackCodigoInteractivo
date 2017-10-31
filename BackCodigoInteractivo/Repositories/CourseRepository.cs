using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsFactory;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.ModelsNotMapped.CoursesModels;
using BackCodigoInteractivo.ModelsNotMapped.CoursesModels.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class CourseRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();
        private CourseModelFactory cmf = new CourseModelFactory();

        public IQueryable<Course> GetAllCourses()
        {

            return ctx.Courses;

        }

        public Course GetCourse(int code)
        {
            return ctx.Courses.Where(c => c.Code == code).FirstOrDefault();
            
        }

        public CourseResponse returnCourseResponse(int codeState = 0, bool status = false, string message = "Está vacio", Object obj = null)
        {
            CourseResponse _cresponse = new CourseResponse();
            _cresponse.codeState = codeState;
            _cresponse.status = status;
            _cresponse.message = message;
            _cresponse.obj = obj;

            return _cresponse;
        }

        public CoursesResponse returnCoursesResponse(int codeState = 0, bool status = false, string message = "Está vacio", ICollection<Course> obj = null)
        {
            CoursesResponse _csr = new CoursesResponse();
            _csr.codeState = codeState;
            _csr.status = status;
            _csr.message = message;
            _csr.courses = obj;

            return _csr;
        }

        public CoursesResponse listCourses()
        {

            try
            {
                ICollection<Course> _courses = GetAllCourses().ToList();

                if (_courses.Count() == 0)
                {
                    return returnCoursesResponse(1,true,"No hay cursos cargados todavia",_courses);
                }

                return returnCoursesResponse(1, true, string.Format("{0} Cursos devueltos correctamente",_courses.Count), _courses);
            }
            catch (Exception e)
            {

                return returnCoursesResponse(0, false, string.Format("Error en la peticion  -> {0}",e.Message));

            }

        }

        public CourseResponse storeCourse (PCourse _pcourse)
        {
            if (_pcourse == null) return returnCourseResponse(0,false,"Está vacia a peticion");

            if (GetCourse(_pcourse.code) != null)
            {
                if (_pcourse.code == 0)
                {
                    return returnCourseResponse(0, false, "El curso debe tener un CODE y este ser diferente de 0");
                }

                return returnCourseResponse(0, false, "El curso con este code ya existe, por favor intentar otro codigo");
            }

            Course _course = new Course();

            try
            {
                if (!string.IsNullOrEmpty(_pcourse.imageBase64) || !string.IsNullOrEmpty(_pcourse.thumbnail))
                {
                    try
                    {
                    if (!base64Upload(_pcourse.imageBase64,_pcourse.thumbnail))
                    {
                        return returnCourseResponse(0, false, "Error de guardado de imagen");
                    }

                }
                catch (Exception e)
                {

                    return returnCourseResponse(0, false, "Error de guardado de imagen -> " + e.Message);
                }


                }
                
                _course.Code = _pcourse.code;
                _course.Name = _pcourse.name;
                _course.Description = _pcourse.description;
                _course.Duration = _pcourse.duration;
                _course.TypeCourse = (TypesCourseEnum)_pcourse.typecourse;  //Parseo.
                _course.Mode = (ModeEnum)_pcourse.mode;
                _course.Level = (LevelEnum)_pcourse.level;
                _course.Video_preview = _pcourse.video_preview;
                _course.Thumbnail = _pcourse.thumbnail;
                _course.ProfessorID = _pcourse.professorId;

                ctx.Courses.Add(_course);
                ctx.SaveChanges();
            }
            catch (Exception)
            {

                return returnCourseResponse(0, false, "Ocurrió un error");
            }

            return returnCourseResponse(1,true,"Creado correctamente", _course);

        }


        public CourseResponse detailCourse(int code)
        {
            if ( code == 0) return returnCourseResponse(0, false, "Está vacia la peticion"); //Si el code es 0, devolverá la peticion inexistente.

            Course _course = ctx.Courses.Where(x => x.Code == code).FirstOrDefault();  //Search if there is some Course with that code.

            if (_course == null) return returnCourseResponse(0, false, "El code no es de un curso existente"); // If there isnt course, so I return json with message error.

            return returnCourseResponse(1, true, "El codigo existe", _course);



        }


        public CourseResponse putCourse(int code,PCourse _pcourse)
        {
            
            if (_pcourse == null && code == 0) return returnCourseResponse(0, false, "Está vacia la peticion"); //Si el code es 0 y la peticion vacia.

            Course _course = ctx.Courses.Where(x => x.Code == code).FirstOrDefault();  //Search if there is some Course with that code.

            if (_course == null) return returnCourseResponse(0, false, "El code no es de un curso existente"); // If there isnt code, so I return json with message error.


            try
            {
                if (!string.IsNullOrEmpty(_pcourse.imageBase64) || !string.IsNullOrEmpty(_pcourse.thumbnail) )
                {
                    try
                    {
                        if (!base64Upload(_pcourse.imageBase64, _pcourse.thumbnail))
                        {
                            return returnCourseResponse(0, false, "Error de guardado de imagen");
                        }

                    }
                    catch (Exception e)
                    {

                        return returnCourseResponse(0, false, "Error de guardado de imagen -> " + e.Message);
                    }

                }

                _course.Code = _pcourse.code;
                _course.Name = _pcourse.name;
                _course.Description = _pcourse.description;
                _course.Duration = _pcourse.duration;
                _course.TypeCourse = (TypesCourseEnum)_pcourse.typecourse;  //Parseo.
                _course.Mode = (ModeEnum)_pcourse.mode;
                _course.Level = (LevelEnum)_pcourse.level;
                _course.Video_preview = _pcourse.video_preview;
                _course.Thumbnail = _pcourse.thumbnail;
                _course.ProfessorID = _pcourse.professorId;

                ctx.Entry(_course).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
            catch (Exception)
            {

                return returnCourseResponse(0, false, "Ocurrió un error en la actualización");
            }

            return returnCourseResponse(1, true, "Creado correctamente", _course);

        }


        public CourseResponse deleteCourse(int code)
        {
            Course _course = ctx.Courses.Where(x => x.Code == code).FirstOrDefault();

            if (_course == null) return returnCourseResponse(0,false,"No existe ningun curso con ese codigo");

            try
            {
                string name = _course.Name;
                ctx.Courses.Remove(_course);
                ctx.SaveChanges();
                return returnCourseResponse(1,true,"Curso eliminado " +name+ " correctamente");
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine(e.Message);
                return returnCourseResponse(0,false,"Ocurrio un error al eliminar");
            }
        }
        public bool base64Upload(string _b64,string imgName)
        {
            try
            {
                var base64Split = _b64.Split(',');  //Recorto el string, antes de "," datos y despues el encode.

                var bytes = Convert.FromBase64String(base64Split[1]);
                string path = System.Web.HttpContext.Current.Server.MapPath(@"~/Uploads/Courses/")+imgName;
                using (var imageFile = new FileStream(path, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}