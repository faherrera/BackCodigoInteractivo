using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackCodigoInteractivo.Repositories.Configs;
namespace BackCodigoInteractivo.Repositories.Auth
{
    public class CredentialsRepository
    {
        private EncryptionsRepository enc = new EncryptionsRepository();

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
    }
}