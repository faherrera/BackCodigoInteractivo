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

        //Credentials

        public bool CredentialsLoginMatch( User user, string password)
        {
            try
            {
                string EncrypPass = Encrypting(password);
                if (user == null) return false;

                if (user.Password != EncrypPass)
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

        //TOKENS
        
        public AuthenticationResponse haveTokenAuth(HttpRequestMessage request)
        {
            AuthenticationResponse authResponse = new AuthenticationResponse();

            string searchInHeader = "Token";

            if (!request.Headers.Contains(searchInHeader)) //Si el Header no trae el Token 
            {
                return authResponse;     
            }

            string Token = request.Headers.GetValues(searchInHeader).FirstOrDefault();

            if (string.IsNullOrEmpty(Token)) return authResponse; //Devuelvo False si está vacio.

            if (!AlreadyExistForToken(Token)) return authResponse; //Devuelvo False si no existe el token

           

            return authResponse = new AuthenticationResponse(true,Token);    //Devuelvo true y sigo la petición.
            
        }


        public AuthenticationResponse haveTokenAuth2(string Token)
        {
            AuthenticationResponse authResponse = new AuthenticationResponse();


            if (string.IsNullOrEmpty(Token)) return authResponse; //Devuelvo False si está vacio.

            if (!AlreadyExistForToken(Token)) return authResponse; //Devuelvo False si no existe el token



            return authResponse = new AuthenticationResponse(true, Token);    //Devuelvo true y sigo la petición.

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