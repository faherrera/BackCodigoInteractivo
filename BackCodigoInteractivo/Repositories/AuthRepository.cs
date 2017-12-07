using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Cors;

namespace BackCodigoInteractivo.Repositories
{
    public class AuthRepository
    {
        protected CodigoInteractivoContext ctx = new CodigoInteractivoContext();


        //Getters

        public User getUserFromUsername(string username)
        {
            return ctx.Users.Where(x => x.Username == username).FirstOrDefault();
        }
        //If Exist
       
        public bool AlreadyExistForUsername(string username) {

            return ctx.Users.Any(x => x.Username == username);

        }

        public bool AlreadyExistForEmail(string email)
        {

            return ctx.Users.Any(x => x.Email == email);

        }

        //Credentials

        public bool CredentialsLoginMatch( User user, string password)
        {
            try
            {
                if (user == null) return false;

                if (user.Password != password)
                {
                    return false;
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public string Encrypting(string pass)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(pass);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);

        }

    }
}