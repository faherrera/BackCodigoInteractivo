using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackCodigoInteractivo.Repositories.Configs;
using BackCodigoInteractivo.DAL;

namespace BackCodigoInteractivo.Repositories.Auth
{
    public class CredentialsRepository
    {
        private EncryptionsRepository enc = new EncryptionsRepository();
        private static CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public bool CredentialsLoginMatch(User user, string password)
        {
            try
            {
                string EncrypPass = enc.Encrypting(password);
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

        public static bool HaveAccess(string Rol, string Token, bool FreeAccess = false)
        {
            // Acceso libre en casos puntuales.
            if (FreeAccess)
            {
                return true;
            }

            if (string.IsNullOrEmpty(Token))
            {
                return false;
            }

            var User = ctx.Users.Where(x => x.Token == Token && x.Role.Title == Rol).FirstOrDefault();

            if (User == null)
            {
                return false;
            }

            return true;


        }
    }
}