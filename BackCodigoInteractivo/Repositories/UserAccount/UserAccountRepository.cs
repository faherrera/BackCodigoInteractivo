using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.ModelsNotMapped.Authentication.General;
using BackCodigoInteractivo.ModelsNotMapped.UserAccount.Request;
using BackCodigoInteractivo.Repositories.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories.UserAccount
{
    public class UserAccountRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();
        private EncryptionsRepository encryRepo = new EncryptionsRepository(); 
        public ResponseRepository UpdateOwnAccount(string Token, UpdateAccountRequest UpdateAccountRequest)
        {

            try
            {
                var User = ctx.Users.FirstOrDefault(x => x.Token == Token);

                if (User == null) return new ResponseRepository("No existe el usuario con ese token, no está autorizado", 402);
                if (string.IsNullOrEmpty(UpdateAccountRequest.Name)) return new ResponseRepository("Debe tener obligatoriamente un nombre", 402);

                User.Name = UpdateAccountRequest.Name;
                User.Email = UpdateAccountRequest.Email;
                User.DNI = UpdateAccountRequest.DNI;
                User.PathProfileImage = MultimediaRepository.base64Upload("Users", UpdateAccountRequest.ImageRequest.Baase64Code, UpdateAccountRequest.ImageRequest.PathProfileImage) ? UpdateAccountRequest.ImageRequest.PathProfileImage : User.PathProfileImage;
                User.Password = string.IsNullOrEmpty(UpdateAccountRequest.Password) ? User.Password : encryRepo.Encrypting(UpdateAccountRequest.Password);
                ctx.Entry(User).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

                return new ResponseRepository("Correctamente modificado",200,new UserLocalStorage(User.Username),true);

            }
            catch (Exception e)
            {

                return new ResponseRepository("Ocurrio un error, comunicarse con el Admin por favor", 500);
            }





        }

    }
}