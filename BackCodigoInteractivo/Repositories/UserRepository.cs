using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace BackCodigoInteractivo.Repositories
{
    public class UserRepository
    {
        CodigoInteractivoContext db = new CodigoInteractivoContext();
        
        public IEnumerable<User> getAllUsers()
        {
            return db.Users;
        }
        
        public IEnumerable<User> getUser(int id)
        {
            User _user = db.Users.Find(id);

            return db.Users.Where(u => u.UserID == _user.UserID).ToList();

           
        }
        
        public Object returnUserJson(int id)
        {
            User _user = db.Users.Find(id);

            Object response;

            if (_user != null)
            {
                response = new
                {
                    user = _user,
                    message = "Existe el usuario " + _user.Username,
                    code = 555 
                };
            }else
            {
                response = new
                {
                    user = false,
                    message = "No existe el registro",
                    code = 666
                };
            }

            return response;
        } 

         
    }
}