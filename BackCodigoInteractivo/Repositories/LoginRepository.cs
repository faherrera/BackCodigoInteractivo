using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.General;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.Login.Request;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.Login.Responses;
using BackCodigoInteractivo.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class LoginRepository : AuthRepository
    {
        LoginResponse _loginResponse;
        UserLocalStorage _userLoginResponse;  //This class has all props that we will return to User.
        CredentialsRepository credentialsRepo = new CredentialsRepository();
        public LoginRepository()
        {
            _loginResponse = null;  
            _userLoginResponse = null;

        }

       public LoginResponse processLogin(LoginRequest userFromBody,string Rol)
        {
            //Consulto si el usuario es nulo.

            using (ctx = new DAL.CodigoInteractivoContext())
            {

                try
                {
                    if (userFromBody == null || string.IsNullOrWhiteSpace(userFromBody.Username)) return _loginResponse = new LoginResponse(null,false,"No puede enviar nulo",0);

                    User _user = ctx.Users.Where(x => x.Username == userFromBody.Username).FirstOrDefault();

                    if (_user == null) return _loginResponse = new LoginResponse(null, false, "El usuario no existe", 404);

                    if (!_user.Availability) return _loginResponse = new LoginResponse(null, false, "El usuario no está disponible para acceder", 401);


                    if (_user.Role.Title.ToUpper() != Rol.ToUpper()) return _loginResponse = new LoginResponse(null, false, "No puede enviar nulo", 401);

                    if (!credentialsRepo.CredentialsLoginMatch(_user, userFromBody.Password)) return _loginResponse = new LoginResponse(null,false,"Las credenciales no coinciden, por favor revisarlas.",0);


                    AuthRepository auth = new AuthRepository();

                    auth.UpdateUserToken(_user,ctx);
                    _userLoginResponse = new UserLocalStorage(_user.Username);

                    return _loginResponse = new LoginResponse(_userLoginResponse,true,"Correctamente logueado",1);


                }
                catch (Exception e)
                {

                    return _loginResponse = new LoginResponse(null, false, string.Format("Error en la petición -> {0}",e.Message), 0);
                }
            }

            
        }

     
    }
}