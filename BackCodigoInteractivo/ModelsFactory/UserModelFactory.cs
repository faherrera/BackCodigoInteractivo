using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsFactory
{
    public class UserModelFactory
    {
        CodigoInteractivoContext db = new CodigoInteractivoContext();
        public User Create(User _user)
        {
            return new User
            {
                UserID = _user.UserID,
                Username = _user.Username,
                Password = _user.Password,
                Name = _user.Name,
                Email = _user.Email,
                Token = _user.Token,
                Role = db.Roles.Find(_user.RolID)
            };

        }

        
    }
}