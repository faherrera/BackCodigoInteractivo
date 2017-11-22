using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Roles.Responses
{
    public class Responses
    {
        public class RoleResponse
        {
            public RoleResponse() { }

            public RoleResponse(String _message = " Error en la petición  ", int _codeState = 0, Role _role = null, bool _status = false)
            {
                this._status = _status;
                this._message = _message;
                this._codeState = _codeState;
                this._role = _role;

            }

            public bool _status { get; set; }
            public string _message { get; set; }
            public int _codeState { get; set; }
            public Role _role { get; set; }

        }
    }

    public class RolesResponse
    {
        public RolesResponse() { }

        public RolesResponse(String _message = " Error en la petición  ", int _codeState = 0, ICollection<Role> _roles = null, bool _status = false)
        {
            this._status = _status;
            this._message = _message;
            this._codeState = _codeState;
            this._roles = _roles;

        }

        public bool _status { get; set; }
        public string _message { get; set; }
        public int _codeState { get; set; }
        public ICollection<Role> _roles { get; set; }

    }
}
