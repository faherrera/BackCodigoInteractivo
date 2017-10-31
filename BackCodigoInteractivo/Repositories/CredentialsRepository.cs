using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;

namespace BackCodigoInteractivo.Repositories
{
    public class CredentialsRepository
    {
        CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public bool existUsername(string Username)
        {
            return ctx.Users.Where(u => u.Username == Username).Any();
            

        }

        public bool validationCredentials(string Username , string Password)
        {
            return ctx.Users.Where(c => c.Username == Username && c.Password == Password).Any();
        }

        public User getUserForUsername(string Username)
        {
            User _user = ctx.Users.Where(c => c.Username == Username).First();

            return _user;
        }

        //In this function I pass 'username' as parameter and The Function return a User Json Object with Code and Message.
        public Object returnUserJson(string username)
        {
            User _user = ctx.Users.Where(u => u.Username == username).FirstOrDefault();
            Object response;        //I Create the Response Object that it will be serialized.

            try
            {
             
                //This Response will return the User, message, success code and boolean. 
                response = new
                {
                    user = _user,
                    message = "Existe el usuario " + _user.Username,
                    code = 555,
                    success = true
                };
                

            }
            catch (Exception)
            {

                response = new
                {
                    message = "No existe el registro",
                    code = 666,
                    success = false
                };
            }


            return response;
        }

        public string returnToken(string username)
        {
            return ctx.Users.Where(t => t.Username == username).First().Token; 
        }

        public Object transformTokenToObject(string message,string token,bool access)
        {
            

            return new {
                        _message = message,
                        _token = token,
                        _access = access
                        };
        }

        public User findUserFromUsername(string username)
        {
            return ctx.Users.Where(u => u.Username == username).First();
        }
        


    }
}