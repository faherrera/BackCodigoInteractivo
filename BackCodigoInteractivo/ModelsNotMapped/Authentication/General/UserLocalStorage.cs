using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Authentication.General
{
    public class UserLocalStorage
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();
         
        public UserLocalStorage(string username)
        {
            var User = ctx.Users.FirstOrDefault();

            this.Name = User.Name;
            this.Username = User.Username;
            this.Email = User.Email;
            this.Token = User.Token;
            this.Image = User.PathProfileImage;

        }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}