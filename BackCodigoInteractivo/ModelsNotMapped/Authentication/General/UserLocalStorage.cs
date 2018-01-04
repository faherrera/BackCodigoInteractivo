using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Authentication.General
{
    public class UserLocalStorage
    {
        public UserLocalStorage(string name,string username, string email, string token)
        {
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Token = token;
        }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}