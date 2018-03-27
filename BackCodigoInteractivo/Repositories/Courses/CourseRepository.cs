using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsFactory;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.ModelsNotMapped.Courses.ModelFactory;
using BackCodigoInteractivo.ModelsNotMapped.CoursesModels;
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
        private CourseResponse _courseR;

        public IQueryable<Course> GetAllCourses()
        {

            return ctx.Courses;

        }

        public Course GetCourse(int code)
        {
            return ctx.Courses.Where(c => c.Code == code).FirstOrDefault();
            
        }

        public CourseResponses listCourses(string rol = "Estudiante")
        {
            CourseResponses courseResponses;

            try
            {
                List<Course> _courses = GetAllCourses().ToList();

                if (_courses.Count() == 0)
                {
                    return courseResponses = new CourseResponses(null,true,"No hay registros cargados aun",4);
                }

                List<CourseModelFactory> listModel = new List<CourseModelFactory>();

                if (rol != "Estudiante")
                {
                    foreach (var course in _courses)
                    {

                        CourseModelFactory _model = new CourseModelFactory
                            (course.Code, course.Name,
                            course.Description, course.Duration,
                            course.TypeCourse, course.Mode,
                            course.Level, course.Video_preview, course.Thumbnail,
                            course.ProfessorID,
                            course.Temary,
                            course.Price,
                            course.StartDate,
                            course.Availability);
                        listModel.Add(_model);
                    }

                }else
                {
                    foreach (var course in _courses)
                    {
                        if (course.Availability)
                        {
                            CourseModelFactory _model = new CourseModelFactory
                                                       (course.Code, course.Name,
                                                       course.Description, course.Duration,
                                                       course.TypeCourse, course.Mode,
                                                       course.Level, course.Video_preview, course.Thumbnail,
                                                       course.ProfessorID,
                                                       course.Temary,
                                                       course.Price,
                                                       course.StartDate,
                                                       course.Availability);
                            listModel.Add(_model);
                        }
                       
                    }
                }
               

                return courseResponses = new CourseResponses(listModel,true,"Traidos los cursos",1);
            }
            catch (Exception e)
            {

                return courseResponses = new CourseResponses(null, false, "Error en la petición -> "+e.Message);

            }

        }

        public CourseResponse storeCourse (PCourse _pcourse)
        {
            if (_pcourse == null) return _courseR = new CourseResponse(null, false, " El curso está vacio");

            if (GetCourse(_pcourse.code) != null || _pcourse.code == 0)
            {
                if (_pcourse.code == 0)
                {
                    return _courseR = new CourseResponse(null, false, " El curso debe tener un codigo y ser diferente de 0");
                }

                return _courseR = new CourseResponse(null, false, " El curso con este codigo ya existe, debe cambiarlo");
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
                            return _courseR = new CourseResponse(null, false, " Error guardando la imagen");
                        }

                    }
                catch (Exception e)
                {

                        return _courseR = new CourseResponse(null, false, "Error en la petición -> " + e.Message);
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
                _course.Temary = _pcourse.temary;
                _course.Price = _pcourse.price;
                _course.StartDate = _pcourse.startDate;

                ctx.Courses.Add(_course);
                ctx.SaveChanges();

                CourseModelFactory _modelFactory = new CourseModelFactory
                        (_course.Code, _course.Name,
                        _course.Description, _course.Duration,
                        _course.TypeCourse, _course.Mode,
                        _course.Level, _course.Video_preview,_course.Thumbnail,
                        _course.ProfessorID,
                        _course.Temary,
                        _course.Price,
                        _course.StartDate
                        );
                return _courseR = new CourseResponse(_modelFactory, true, "Correctamente guardado", 1);

            }
            catch (Exception e)
            {

                return _courseR = new CourseResponse(null, false, "Error en la petición -> " + e.Message, 0);
            }


        }


        public CourseResponse detailCourse(int code)
        {
            CourseResponse courseResponse;

            CourseModelFactory _cfactory;

            try
            {
                if (code == 0) return courseResponse = new CourseResponse(null,false,"El codigo no puede ser igual a 0"); //Si el code es 0, devolverá la peticion inexistente.

                Course _course = ctx.Courses.Where(x => x.Code == code).FirstOrDefault();  //Search if there is some Course with that code.

                if (_course == null) return courseResponse = new CourseResponse(null, false, "No existe codigo con ese numero"); // If there isnt course, so I return json with message error.

                _cfactory = new CourseModelFactory
                        (_course.Code, _course.Name,
                        _course.Description, _course.Duration,
                        _course.TypeCourse, _course.Mode,
                        _course.Level, _course.Video_preview,
                        _course.Thumbnail,
                        _course.ProfessorID,
                        _course.Temary,
                        _course.Price,
                        _course.StartDate,
                        _course.Availability
                        );

                return courseResponse = new CourseResponse(_cfactory,true,"Correctamente traido",1);

            }
            catch (Exception e)
            {

                return courseResponse = new CourseResponse(null, false, string.Format("Error en la petición -> {0}",e.Message));

            }
        }


        public CourseResponse putCourse(int code,PCourse _pcourse)
        {
            CourseResponse courseResponse;

            if (_pcourse == null || code == 0) return courseResponse = new CourseResponse(null, false, string.Format("Error en la petición"));


            Course _course = ctx.Courses.Where(x => x.Code == code).FirstOrDefault();  //Search if there is some Course with that code.

            if (_course == null) return courseResponse = new CourseResponse(null, false, "No existe ningun curso con ese code", 0);


            try
            {
                if (!string.IsNullOrEmpty(_pcourse.imageBase64) || !string.IsNullOrEmpty(_pcourse.thumbnail) )
                {
                    try
                    {
                        if (!base64Upload(_pcourse.imageBase64, _pcourse.thumbnail))
                        {
                            return courseResponse = new CourseResponse(null, false, "Error guardando la imagen", 0);
                        }

                    }
                    catch (Exception e)
                    {

                        return courseResponse = new CourseResponse(null, false, "Error en la petición -> " + e.Message, 0);
                    }

                }

                _course.Name = _pcourse.name;
                _course.Description = _pcourse.description;
                _course.Duration = string.IsNullOrEmpty(_pcourse.duration) ? _course.Duration : _pcourse.duration;
                _course.TypeCourse = (TypesCourseEnum)_pcourse.typecourse;  //Parseo.
                _course.Mode = (ModeEnum)_pcourse.mode;
                _course.Level = (LevelEnum)_pcourse.level;
                _course.Video_preview = _pcourse.video_preview;
                _course.Thumbnail = string.IsNullOrEmpty(_pcourse.thumbnail) ? _course.Thumbnail : _pcourse.thumbnail;
                _course.ProfessorID = _pcourse.professorId;
                _course.Temary = _pcourse.temary;
                _course.Price = _pcourse.price;
                _course.StartDate = _pcourse.startDate;

                ctx.Entry(_course).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

                CourseModelFactory _modelFactory = new CourseModelFactory
                        (_course.Code, _course.Name,
                        _course.Description, _course.Duration,
                        _course.TypeCourse, _course.Mode,
                        _course.Level, _course.Video_preview,_course.Thumbnail,
                        _course.ProfessorID,_course.Temary,_course.Price,_course.StartDate);
                return courseResponse = new CourseResponse(_modelFactory, true, "Correctamente guardado",1);

            }
            catch (Exception e)
            {

                return courseResponse = new CourseResponse(null, false, "Error en la petición -> " + e.Message);
            }


        }


        public CourseResponse deleteCourse(int code)
        {
            Course _course = ctx.Courses.Where(x => x.Code == code).FirstOrDefault();
            CourseResponse _cresponse;
            if (_course == null) return _cresponse = new CourseResponse(null,false,"No existe ningun curso con ese code",0);

            try
            {
                string name = _course.Name;

                ///En caso de que haya inscripciones.
                if (ctx.UsersCourses.Any(x=> x.CourseID == _course.Code))
                {
                    ChangeToUnAvailable(_course);

                    return _cresponse = new CourseResponse(null, true, string.Format("El curso {0} no puede ser eliminado definitivamente ya que posee alumnos inscriptos, desinscribalos previamente, su estado cambió a NO DISPONIBLE",name), 1);
                }

                ctx.Courses.Remove(_course);
                ctx.SaveChanges();

                return _cresponse = new CourseResponse(null,true,"Eliminado correctamente "+name ,1);
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine(e.Message);
                return _cresponse = new CourseResponse(null,false,string.Format("Error en la petición -> {0}",e));
            }
        }

        private void ChangeToUnAvailable(Course course)
        {
            course.Availability = false;
            ctx.Entry(course).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public CourseResponse ChangeAvailability(int code)
        {
            try
            {
                var course = ctx.Courses.FirstOrDefault(x => x.Code == code);

                course.Availability = !course.Availability;

                ctx.Entry(course).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

                return new CourseResponse(null, true, string.Format("Cambiada Disponbilidad de {0} correctamente", course.Name), 1);

            }
            catch (Exception e)
            {

                return new CourseResponse(null, false, string.Format("Ocurrio un problema al querer eliminar al curso {0}", e.Message),0);
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