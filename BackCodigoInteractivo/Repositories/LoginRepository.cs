using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.Login.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class LoginRepository : AuthRepository
    {
        LoginResponse _loginResponse;
        UserLoginResponse _userLoginResponse;  //This class has all props that we will return to User.

        public LoginRepository()
        {
            _loginResponse = null;  
            _userLoginResponse = null;

        }

       public LoginResponse processLogin( User userFromBody)
        {
            //Consulto si el usuario es nulo.

            try
            {
                if (userFromBody == null || string.IsNullOrWhiteSpace(userFromBody.Username)) return _loginResponse = new LoginResponse(null,false,"No puede enviar nulo",0);

                User _user = getUserFromUsername(userFromBody.Username);

                if (!CredentialsLoginMatch(_user, userFromBody.Password)) return _loginResponse = new LoginResponse(null,false,"Las credenciales no coinciden, por favor revisarlas.",0);

                _userLoginResponse = new UserLoginResponse(_user.Username,_user.Email,_user.Token);

                return _loginResponse = new LoginResponse(_userLoginResponse,true,"Correctamente logueado",1);


            }
            catch (Exception e)
            {

                return _loginResponse = new LoginResponse(null, false, string.Format("Error en la petición -> {0}",e.Message), 0);
            }

            
        }
    }
}