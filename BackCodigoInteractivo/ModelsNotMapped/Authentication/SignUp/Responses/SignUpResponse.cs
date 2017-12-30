using BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Authentication.SignUp.Responses
{
    public class SignUpResponse : BaseResponse
    {
        public SignUpResponse() { }

        public SignUpResponse(Object data, bool status, string message, int codeState)
        {
            this.data = data;
            this.status = status;
            this.message = message;
            this.codeState = codeState;
        }
    }
}