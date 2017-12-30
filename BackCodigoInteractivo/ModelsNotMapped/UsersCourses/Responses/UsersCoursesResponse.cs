using BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses;
using BackCodigoInteractivo.ModelsNotMapped.UsersCourses.ModelFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.UsersCourses.Responses
{
    public class UsersCoursesResponse : BaseResponse
    {
        public UsersCoursesResponse() { }
        public UsersCoursesResponse(String message = " Error en la petición  ", int codeState = 0, Object data = null, bool status = false)
        {
            this.status = status;
            this.message = message;
            this.codeState = codeState;
            this.data = data;

        }
    }

    public class ListUsersCoursesResponse : BaseResponses
    {
        public ListUsersCoursesResponse() { }
        public ListUsersCoursesResponse(String message = " Error en la petición  ", int codeState = 0, List<UserCourseModelFactory> data = null, bool status = false)
        {
            this.status = status;
            this.message = message;
            this.codeState = codeState;
            this.data = data;
        }

        public List<UserCourseModelFactory> data { get; set; }
    }
}