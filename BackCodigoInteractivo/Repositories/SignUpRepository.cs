using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.General;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.SignUp.Request;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.SignUp.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class SignUpRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();
        private SignUpResponse response = null;
        private AuthRepository auth = new AuthRepository(); //Uso sus metodos.
        private UserLocalStorage userLocalStorage = null;

        public SignUpResponse processingSignup( SignUpRequest request)
        {
            try
            {

                if (request == null) return response = new SignUpResponse(null,false,"La consulta vino vacia",0);

                //En caso de que esté ocupado ya el user.
                if (auth.AlreadyExistForUsername(request.Username)) return response = new SignUpResponse(null,false,"Ya existe el usuario ingresado",2);
            
                //En caso de que esté ocupado ya el email
                if (auth.AlreadyExistForEmail(request.Email)) return response = new SignUpResponse(null, false, "El email ingresado ya está ocupado", 2);

                
             
                return response = new SignUpResponse(storeUser(request), true,"traido correctamente",1);


            }
            catch (Exception e)
            {

                return response = new SignUpResponse(null, false, " Error en al petición  "+e.Message, 0);
                
            }
             


        }

        //Aquí guardo el User y devuelvo lo que entraría en la respuesta del local storage.
        public UserLocalStorage storeUser(SignUpRequest request)
        {
            User user = parseSignUpToUser(request);

            ctx.Users.Add(user);
            ctx.SaveChanges();

            return userLocalStorage = new UserLocalStorage(user.Name,user.Username, user.Email, user.Token);


        }

        public User parseSignUpToUser(SignUpRequest request)
        {
            Random rnd = new Random();

            User user = new User();

            user.Username = request.Username;
            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = auth.Encrypting(request.Password); ///Debería llamar a un metodo que encripte la contraseña.
            user.DNI = request.DNI;

            System.Diagnostics.Debug.WriteLine(request.Password);
            System.Diagnostics.Debug.WriteLine(user.Password);

            //Valores que ingreso por defecto
            user.RolID = 1; //Sería cliente normal.
            user.Token = rnd.Next(10000, 9999999).ToString();   //Le doy un number random para simular token. 

            return user;
        } 

    }
}