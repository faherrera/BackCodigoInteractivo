using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;

namespace BackCodigoInteractivo.Repositories
{
    public class AuthRepository
    {
        CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public bool existsToken(string token)   //Consultar si existe el token.
        {
            return ctx.Users.Where(u => u.Token == token).Any();
        }

        public UserResponse returnUserView(string token)
        {
            User _userDB = ctx.Users.Where(u => u.Token == token).FirstOrDefault();

            UserResponse _userResponse = new UserResponse();

            _userResponse.Name = _userDB.Name;
            _userResponse.Email = _userDB.Email;
            _userResponse.PathPhoto = null;
            _userResponse.Token = _userDB.Token;

            return _userResponse;
        }

        public string tokenReceivedFromHeader(IEnumerable<string> getValuesFromHeader )
        {
            return getValuesFromHeader.FirstOrDefault();
        }


    }
}