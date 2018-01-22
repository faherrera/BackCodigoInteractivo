using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses;
using BackCodigoInteractivo.ModelsNotMapped.Users.ModelFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Users.Responses
{
    public class Responses
    {
        public class UserResponse : BaseResponse
        {
            public UserResponse() { }
            public UserResponse( String _message = " Error en la petición  ", int _codeState = 0, Object _user = null, bool _status = false)
            {
                this.status = _status;
                this.message = _message;
                this.codeState = _codeState;
                this.data = _user;

            }
           

        }

        public class UsersResponse : BaseResponses
        {
            public UsersResponse()
            {
            }

            public UsersResponse(ICollection<UserModelFactory> _users = null, string _message = " Error en la petición  ", int _codeState = 0,bool _status = false)
            {
                this.status = _status;
                this.message = _message;
                this.codeState = _codeState;
                this.users = _users;

            }

            public ICollection<UserModelFactory> users { get; set; }

        }
    }
}