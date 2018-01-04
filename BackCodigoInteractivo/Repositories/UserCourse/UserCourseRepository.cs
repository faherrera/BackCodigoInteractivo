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

        User user = new User();
        

        public List<UserCourseModelFactory> ListingCoursesAccordingTo(int UserID)
        {
            List<UserCourseModelFactory> listUserModelFactory = new List<UserCourseModelFactory>();

            var userCourses = ctx.UsersCourses.Where(x => x.UserID == UserID).ToList();

            foreach (var uc in userCourses)
            {
                userCourseModelFactory = new UserCourseModelFactory(uc.UserCourseID,uc.UserID,uc.CourseID,uc.Access,uc.pathCertificate,uc.isInstructor);

                listUserModelFactory.Add(userCourseModelFactory);
            }

            return listUserModelFactory;
        }

        public ListUsersCoursesResponse ListAllCoursesForEachUser(string Username) {

            try
            {
                if (String.IsNullOrWhiteSpace(Username)) return listUserCourseResponse = new ListUsersCoursesResponse("La petición debe contener un Username valido", 2);

                var user = ctx.Users.Where(x => x.Username == Username).FirstOrDefault();

                if (user == null) return listUserCourseResponse = new ListUsersCoursesResponse("No existe el User solicitado", 2);

                return listUserCourseResponse = new ListUsersCoursesResponse("Petición Correcta",1,ListingCoursesAccordingTo(user.UserID),true);
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
                if(!validationUserCourse.existUserAndCourse(userCourseRequest)) return userCourseResponse = new UsersCoursesResponse("La petición debe contener usuario y curso existentes ");

                UserAndCourse userAndCourse = new UserAndCourse(userCourseRequest.Username,userCourseRequest.CourseCode);

                User_Course _userCourse = new User_Course();

                _userCourse.UserID = userAndCourse.user.UserID;
                _userCourse.CourseID = userAndCourse.course.Code;
                _userCourse.Access = userCourseRequest.Access;
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

        

        
    }
}