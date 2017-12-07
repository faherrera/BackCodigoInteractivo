using BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Authentication.Login.Responses
{
    public class LoginResponse:BaseResponse
    {
        public LoginResponse() { }

        public LoginResponse(Object data,bool status,string message,int codeState) {
            this.data = data;
            this.status = status;
            this.message = message;
            this.codeState = codeState;
        }


    }
}