using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories.Admin
{
    public class AdminRepository
    {
        static private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public static bool HaveAccess(string Rol,string Token, bool FreeAccess = false)
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