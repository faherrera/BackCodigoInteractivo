using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Users.Responses
{
    public class Responses
    {
        public class UserResponse
        {
            public UserResponse() { }
            public UserResponse( String _message = " Error en la petición  ", int _codeState = 0, User _user = null, bool _status = false)
            {
                this._status = _status;
                this._message = _message;
                this._codeState = _codeState;
                this._user = _user;

            }

            public bool _status { get; set; }
            public string _message { get; set; }
            public int _codeState { get; set; }
            public User _user { get; set; }

        }

        public class UsersResponse
        {
            public UsersResponse()
            {
            }

            public UsersResponse(ICollection<User> _users = null, String _message = " Error en la petición  ", int _codeState = 0,bool _status = false)
            {
                this._status = _status;
                this._message = _message;
                this._codeState = _codeState;
                this._users = _users;

            }

           

            public bool _status { get; set; }
            public string _message { get; set; }
            public int _codeState { get; set; }
            public ICollection<User> _users { get; set; }

        }
    }
}