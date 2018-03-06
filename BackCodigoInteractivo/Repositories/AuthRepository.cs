using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Cors;
using BackCodigoInteractivo.Repositories.Configs;

namespace BackCodigoInteractivo.Repositories
{
    public class AuthRepository
    {
        protected CodigoInteractivoContext ctx = new CodigoInteractivoContext();
        private EncryptionsRepository enc = new EncryptionsRepository();


        //Getters

        public User getUserFromUsername(string username)
        {
            return ctx.Users.Where(x => x.Username == username).FirstOrDefault();
        }

        public User getUserFromToken(string Token)
        {
            return ctx.Users.Where(x => x.Token == Token).FirstOrDefault();

        }

        public User getUserFromID(int id)
        {
            return ctx.Users.Where(x => x.UserID == id).FirstOrDefault();
        }


        //If Exist

        public bool AlreadyExistForUsername(string username) {

            return ctx.Users.Any(x => x.Username == username);

        }

        public bool AlreadyExistForEmail(string email)
        {

            return ctx.Users.Any(x => x.Email == email);

        }

        public bool AlreadyExistForToken(string Token)
        {

            return ctx.Users.Any(x => x.Token == Token);

        }



        public User UpdateUserToken(User user,CodigoInteractivoContext ctx)
        {
            user.Token = GenerateToken();

            ctx.Entry(user).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();

            return user;
        }

        public string GenerateToken()
        {
            string tok = Guid.NewGuid().ToString();

            return tok.Replace("-", "");

        }


    }
}