using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BackCodigoInteractivo.Repositories.Configs
{
    public class EncryptionsRepository
    {
        public string Encrypting(string pass)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(pass);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);

        }
    }
}