using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.General;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.ModelFactory;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Responses;
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
        AuthRepository authRepo;
        UsersCoursesResponse userCourseResponse = new UsersCoursesResponse();
        ListUsersCoursesResponse listUserCourseResponse = new ListUsersCoursesResponse();

        UserCourseModelFactory userCourseModelFactory = new UserCourseModelFactory();

        User user;
        Course course; 

        public UserCourseRepository()
        {
            user = new User();
            authRepo = new AuthRepository();
             
        }


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

        public ListUsersCoursesResponse ListAllCoursesForEachUser(HttpRequestMessage request,string Username) {

            try
            {
                var authToken = authRepo.haveTokenAuth(request);

                if (!authToken.status) { return listUserCourseResponse = new ListUsersCoursesResponse("No posee los permisos para generar esta solicitud", 401); }

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

        public UsersCoursesResponse detailUserCourse(HttpRequestMessage request, int UserCourseID)
        {
            try
            {
                var authToken = authRepo.haveTokenAuth(request);

                if (!authToken.status) { return userCourseResponse = new UsersCoursesResponse("No posee los permisos para generar esta solicitud", 401); }

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
        public UsersCoursesResponse linkUserAndCourse(HttpRequestMessage request , User_Course userCourse)
        {
            try
            {
                var authToken = authRepo.haveTokenAuth(request);
                if (!authToken.status){ return userCourseResponse = new UsersCoursesResponse("No posee los permisos para generar esta solicitud", 401); }

                var userAndCourse = searchUserAndCourse(authToken.token,userCourse.CourseID);

                userCourse.UserID = userAndCourse.user.UserID;

                ctx.UsersCourses.Add(userCourse);
                ctx.SaveChanges();

                //Retorno el response satisfactorio.

                return userCourseResponse = new UsersCoursesResponse("Petición correcta ",1,null,true);

            }
            catch (Exception e)
            {

                return userCourseResponse = new UsersCoursesResponse("Este es el error en la petición -> "+e.Message);

            }


        }

        public UserCourseResponse searchUserAndCourse(string TokenUser, int codeCourse) {
            user = authRepo.getUserFromToken(TokenUser);

            course = ctx.Courses.Where(x=> x.Code == codeCourse).FirstOrDefault();


            UserCourseResponse response = new UserCourseResponse(user,course);
                
            return response;
        }
    }
}