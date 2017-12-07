using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Authentication.Login.Responses
{
    public class UserLoginResponse
    {
        
        public UserLoginResponse( string username , string email,string token)
        {
            this.Username = username;
            this.Email = email;
            this.Token = token;
        }
        public string Username{ get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}