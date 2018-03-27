using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.General;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.ModelFactory;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Request;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Responses;
using BackCodigoInteractivo.Repositories.Courses;
using BackCodigoInteractivo.Repositories.UserCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class UserCourseRepository
    {
        CodigoInteractivoContext ctx = new CodigoInteractivoContext();
        AuthRepository authRepo = new AuthRepository();
        UsersCoursesResponse userCourseResponse = new UsersCoursesResponse();
        ListUsersCoursesResponse listUserCourseResponse = new ListUsersCoursesResponse();
        ValidationUserCourseRepository validationUserCourse = new ValidationUserCourseRepository();

        UserCourseModelFactory userCourseModelFactory = new UserCourseModelFactory();

        public List<UserCourseEnrollment> GetAllUserCourses()
        {
            var ListUserCourse = ctx.UsersCourses.ToList();

            List<UserCourseEnrollment> ListEnrollment = new List<UserCourseEnrollment>();
            foreach (var item in ListUserCourse)
            {
                UserCourseEnrollment Enrollment = new UserCourseEnrollment()
                {
                     UserCourseID = item.UserCourseID,
                     Username = item.User.Username,
                      Access = item.Access,
                    IsInstructor = item.isInstructor,
                     CourseName = ctx.Courses.FirstOrDefault(x=> x.Code == item.CourseID).Name,
                      CourseCode = item.CourseID,
                };

                ListEnrollment.Add(Enrollment);
            }
            return ListEnrollment;
        }

        public User_Course GetUserCourseFromCourseCodeAndUsername(int CourseCode, string Username)
        {
            return ctx.UsersCourses.Where(x => x.CourseID == CourseCode && x.User.Username == Username).FirstOrDefault();

        }

        public List<User_Course> getListUserCourseForBooleanParams(int UserID,string Filter, string FilterValue)
        {
            List<User_Course> listUC = new List<User_Course>();
            bool value = (FilterValue.ToUpper() == "TRUE") ? true : false;

            switch (Filter.ToLower())
            {
                case "access":
                    return listUC = ctx.UsersCourses.Where(x => x.UserID == UserID && x.Access == value).ToList();
                case "instructor":
                    return listUC = ctx.UsersCourses.Where(x => x.UserID == UserID && x.isInstructor == true).ToList();
                default:
                    return listUC = ctx.UsersCourses.Where(x => x.UserID == UserID && x.Access == value).ToList();

            }
        } 


        public List<UserCourseModelFactory> ListingCoursesAccordingTo(int UserID,string Filter, string FilterValue)
        {
            List<UserCourseModelFactory> listUserModelFactory = new List<UserCourseModelFactory>();

            var userCourses = getListUserCourseForBooleanParams(UserID,Filter,FilterValue);

            foreach (var uc in userCourses)
            {
                userCourseModelFactory = new UserCourseModelFactory(uc.UserCourseID,uc.UserID,uc.CourseID,uc.Access,uc.pathCertificate,uc.isInstructor);

                listUserModelFactory.Add(userCourseModelFactory);
            }

            return listUserModelFactory;
        }

        public ListUsersCoursesResponse ListAllCoursesForEachUser(string Username,string Filter = "Access",string valueFilter = "true") {

            try
            {
                if (String.IsNullOrWhiteSpace(Username)) return listUserCourseResponse = new ListUsersCoursesResponse("La petición debe contener un Username valido", 2);

                var user = ctx.Users.Where(x => x.Username == Username).FirstOrDefault();

                if (user == null) return listUserCourseResponse = new ListUsersCoursesResponse("No existe el User solicitado", 2);

                return listUserCourseResponse = new ListUsersCoursesResponse("Petición Correcta",1,ListingCoursesAccordingTo(user.UserID,Filter, valueFilter),true);
            }
            catch (Exception e )
            {

                return listUserCourseResponse = new ListUsersCoursesResponse("Petición Incorrecta, el error es el siguiente -> "+e.Message, 0);

            }


        }

        public UsersCoursesResponse detailUserCourse(int UserCourseID)
        {
            try
            {
                if (UserCourseID == 0) { return userCourseResponse = new UsersCoursesResponse("Debe ingresar un codigo para la solicitud", 0); }

                var userCourse = ctx.UsersCourses.Find(UserCourseID);

                if(userCourse == null) return userCourseResponse = new UsersCoursesResponse("No existe el recurso solicitado",2);


                userCourseModelFactory = new UserCourseModelFactory(userCourse.UserCourseID,userCourse.UserID,userCourse.CourseID,userCourse.Access,userCourse.pathCertificate,userCourse.isInstructor);

                return userCourseResponse = new UsersCoursesResponse("Recurso traido correctamente",1,userCourseModelFactory,true);

            }
            catch (Exception e)
            {
                return userCourseResponse = new UsersCoursesResponse("Error en al petición, el error es -> "+e.Message, 0);
            }

        }

        //Eso será via Post desde el controller
        public UsersCoursesResponse linkUserAndCourse(LinkUserCourseRequest userCourseRequest)
        {
            try
            {
                var isAdmin = ctx.Users.Any(x => x.Username == userCourseRequest.Username && x.Role.Title == "Administrador");

                if (isAdmin) return userCourseResponse = new UsersCoursesResponse("No puede inscribirse si es administrador");

                if (!validationUserCourse.existUserAndCourse(userCourseRequest)) return userCourseResponse = new UsersCoursesResponse("La petición debe contener usuario y curso existentes ");



                UserAndCourse userAndCourse = new UserAndCourse(userCourseRequest.Username,userCourseRequest.CourseCode);

                if(validationUserCourse.alreadyEnroled(userAndCourse.user.UserID,userAndCourse.course.Code)) return userCourseResponse = new UsersCoursesResponse("Ya está inscripto a este curso", 404);

                var Course = ctx.Courses.FirstOrDefault(x => x.Code == userCourseRequest.CourseCode);

                User_Course _userCourse = new User_Course();

                _userCourse.UserID = userAndCourse.user.UserID;
                _userCourse.CourseID = userAndCourse.course.Code;
                _userCourse.Access = Course.TypeCourse == (TypesCourseEnum)1 ? true : false;
                _userCourse.isInstructor = userCourseRequest.isInstructor;
                 
                ctx.UsersCourses.Add(_userCourse);
                ctx.SaveChanges();

                //Retorno el response satisfactorio.

                return userCourseResponse = new UsersCoursesResponse("Petición correcta ",1,null,true);

            }
            catch (Exception e)
            {

                return userCourseResponse = new UsersCoursesResponse("Este es el error en la petición -> "+e.Message);

            }


        }


        public UsersCoursesResponse UpdateBool(int CourseCode, string Username,string Put)
        {
            using (ctx = new CodigoInteractivoContext()) //Instancio un nuevo contexto por si hay varias consultas al mismo tiempo.
            {
                try
                {
                    var UserCourse = GetUserCourseFromCourseCodeAndUsername(CourseCode, Username);

                    if (UserCourse == null) return userCourseResponse = new UsersCoursesResponse("No existe el registro de inscripcion solicitado", 404);


                    if (string.IsNullOrEmpty(Put)) return userCourseResponse = new UsersCoursesResponse("No puede ejecutar esa solicitud", 404);

                    switch (Put.ToLower())
                    {
                        case "access":
                            UserCourse.Access = !UserCourse.Access;
                            break;
                        case "professor":
                            UserCourse.isInstructor = !UserCourse.isInstructor;
                            break;
                        default:
                            return userCourseResponse = new UsersCoursesResponse("No puede ejecutar esa solicitud", 404);

                    }

                    if (UserCourse.isInstructor) UserCourse.Access = true;

                    ctx.Entry(UserCourse).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                    return userCourseResponse = new UsersCoursesResponse(string.Format("Cambiado {0} correctamente", Put), 200, null, true);

                }
                catch (Exception e )
                {

                    return userCourseResponse = new UsersCoursesResponse("Error en el proceso ->  "+e.Message, 500);

                }


            }

        }

        public UsersCoursesResponse DeleteUserCourse(int UserCourseID)
        {
            using (ctx = new CodigoInteractivoContext())
            {
                try
                {
                    var userCourse = ctx.UsersCourses.Find(UserCourseID);

                    if (userCourse == null)
                    {
                        userCourseResponse = new UsersCoursesResponse("Debe ingresar un id existente para eliminar", 404);
                    }

                    ctx.Entry(userCourse).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();

                    return new UsersCoursesResponse("Correctamente eliminado",200,null,true);

                }
                catch (Exception e)
                {
                    return userCourseResponse = new UsersCoursesResponse(string.Format("Error al eliminar -> {0}",e.Message), 500);

                }

            }
        }

        public UsersCoursesResponse DeleteFromCourseCode(int CourseCode,string Token)
        {
            try
            {

                var UserCourse = ctx.UsersCourses.Where(x => x.CourseID == CourseCode && x.User.Token == Token).FirstOrDefault();
<<<<<<< HEAD

                if (UserCourse == null)
                {
                    return new UsersCoursesResponse("No existe el registro solicitado",404,null,false);
                }

                ctx.UsersCourses.Remove(UserCourse);
                ctx.SaveChanges();

=======

                if (UserCourse == null)
                {
                    return new UsersCoursesResponse("No existe el registro solicitado",404,null,false);
                }

                ctx.UsersCourses.Remove(UserCourse);
                ctx.SaveChanges();

>>>>>>> testing
                return new UsersCoursesResponse("Registro eliminado correctamente", 200, null, true);

            }
            catch (Exception e)
            {

                return new UsersCoursesResponse("Ocurrio algo inesperado, por favor contacte con el admin "+e.Message, 500, null, false);
            }
        }
        /// <summary>
        /// Saber si está inscripto, en caso de serlo devolverá true para poder hacer la peticion.
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="CourseCode"></param>
        /// <returns></returns>
        public bool BeEnrroled(string Token, int ClassCode)
        {
            var User = ctx.Users.FirstOrDefault(x => x.Token == Token);
            var Class = ctx.Classes.FirstOrDefault(x => x.CodeClass == ClassCode);

            if (User == null || Class == null)
            {
                return false;
            }

            return ctx.UsersCourses.Any(x => x.CourseID == Class.CourseID && x.UserID == User.UserID && x.Access == true);

        }

    }
}