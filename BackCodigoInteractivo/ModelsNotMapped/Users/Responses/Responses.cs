using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses;
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
            public UserResponse( String _message = " Error en la petición  ", int _codeState = 0, User _user = null, bool _status = false)
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

            public UsersResponse(ICollection<User> _users = null, String _message = " Error en la petición  ", int _codeState = 0,bool _status = false)
            {
                this.status = _status;
                this.message = _message;
                this.codeState = _codeState;
                this.users = _users;

            }

            public ICollection<User> users { get; set; }

        }
    }
}