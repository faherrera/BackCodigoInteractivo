using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Users.ModelFactory;
using BackCodigoInteractivo.ModelsNotMapped.Users.Responses;
using BackCodigoInteractivo.Repositories.Configs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using static BackCodigoInteractivo.ModelsNotMapped.Users.Responses.Responses;

namespace BackCodigoInteractivo.Repositories
{
    public class UserRepository
    {
        public CodigoInteractivoContext db = new CodigoInteractivoContext();
        public UserModelFactory _userModelFactory = null;
        EncryptionsRepository encryptrepo;

        public ICollection<User> getAllUsers()
        {
            return db.Users.ToList();
        }
        
        public User getUserById(int id)
        {
            return db.Users.Find(id);
            
        }

        public User getUserByEmail(String _string)
        {
            return db.Users.Where(x => x.Email == _string).FirstOrDefault();
        }
        
       
        public bool busyUsername(string _username)
        {
            return (string.IsNullOrWhiteSpace(_username)) ? false : db.Users.Any(x => x.Username == _username);   //En caso de ser vacio retornaré true porque es posible no tener un Username definido aún.
        }

        public bool busyEmail(string _email)
        {

            return (string.IsNullOrWhiteSpace(_email)) ? false : db.Users.Any(x => x.Email == _email);   //En caso de ser vacio retornaré true porque es posible no tener un email definido aún.
        }

        private void ChangeToUnAvailable(User user)
        {
            user.Availability = false;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }


        public UserResponse ChangeAvailability(int code)
        {
            {
                try
                {
                    var user = db.Users.FirstOrDefault(x => x.UserID == code);

                    user.Availability = !user.Availability;

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return new UserResponse(string.Format("Cambiada Disponbilidad de {0} correctamente", user.Name),1,null, true);

                }
                catch (Exception e)
                {

                    return new UserResponse("Problema al cambiar la disponibildad del usuario "+e.Message,0,null,false);
                }

            }
        }

        public ICollection<CourseBelongToModelFactory> GetCourseBelonging(int UserID)
        {
            var UserCourse = db.UsersCourses.Where(x => x.UserID == UserID).ToList();

            List<CourseBelongToModelFactory> ListCourseBelong = new List<CourseBelongToModelFactory>();

            foreach (var item in UserCourse)
            {
                CourseBelongToModelFactory CourseBelong = new CourseBelongToModelFactory {
                     Name = db.Courses.Where(x=> x.Code == item.CourseID).First().Name,
                     CourseCode = item.CourseID,
                     IsProfessor = item.isInstructor,
                     Access = item.Access
                };

                ListCourseBelong.Add(CourseBelong);
            }

            return ListCourseBelong;
        }
        //To controller
        public UsersResponse listUsers() {
           UsersResponse _usersRes;
           
            try
            {
                if (getAllUsers().Count() == 0)
                {
                    return _usersRes = new UsersResponse(null, "Aún no hay usuarios cargados", 404);
                }

                List<UserModelFactory> ListUserModelFactory = new List<ModelsNotMapped.Users.ModelFactory.UserModelFactory>();

                foreach (var item in getAllUsers())
                {
                    Debug.WriteLine(item.Role.Title);
                    _userModelFactory = new UserModelFactory(item.UserID, item.Username, item.Name, item.Email, item.PathProfileImage, item.Role.Title,item.DNI,item.RoleID, GetCourseBelonging(item.UserID), item.Availability);


                    ListUserModelFactory.Add(_userModelFactory);

                }
                return _usersRes = new UsersResponse(ListUserModelFactory,"Listado traido correctamente",1,true);

            }
            catch (Exception e)
            {

                return _usersRes = new UsersResponse(null, String.Format("Error en la petición -> {0}", e.Message));
            }



        }
        
        public UserResponse detailUser(int id)
        {
            UserResponse _userRes;
            try
            {
                User _us = getUserById(id);

                if (_us == null) return _userRes = new UserResponse("No existe el usuario con ese ID",2);

                return _userRes = new UserResponse("User traido correctamente",1, _userModelFactory = new UserModelFactory(_us.UserID,_us.Username,_us.Name,_us.Email,_us.PathProfileImage,_us.Role.Title,_us.DNI,_us.RoleID, GetCourseBelonging(_us.UserID), _us.Availability),true);
            }
            catch (Exception e)
            {
                return _userRes = new UserResponse("Error en la peticion, intente nuevamente por favor. -> " + e.Message);
            }
        }

        public UserResponse storeUser(UserFromFrontend UFF)
        {
            UserResponse _userRes;
            encryptrepo = new EncryptionsRepository();
            try
            {
                if (UFF == null) return _userRes = new UserResponse("Error, debe enviar los datos.");

                if (busyEmail(UFF.User.Email)) return _userRes = new UserResponse("Error, este Email ya está ocupado.");

                if (busyUsername(UFF.User.Username)) return _userRes = new UserResponse("Error, este Username ya está ocupado.");

                if (!MultimediaRepository.base64Upload("Users", UFF.imageBase64, UFF.thumbnail)) return _userRes = new UserResponse("Error en la carga de las imagenes, intente nuevamente por favor",400);

                UFF.User.PathProfileImage = UFF.thumbnail;
                UFF.User.Password = encryptrepo.Encrypting(UFF.User.Password);

                db.Users.Add(UFF.User);
                db.SaveChanges();

                return _userRes = new UserResponse("Cargado correctamente",1,null,true);

            }
            catch (Exception e)
            {

                return _userRes = new UserResponse("Error en la peticion, intente nuevamente por favor. -> " +e.Message );
            }


        }
            
        public UserResponse putUser(int id, UserFromFrontend UFF)
        {
            UserResponse _user;
            encryptrepo = new EncryptionsRepository();

            try
            {
               

                if(UFF == null) { return _user = new UserResponse("No puede estár vacia la petición",2); }

              

                if (getUserById(id) == null) { return _user = new UserResponse("No existe ningún Usuario con ese ID",404); }

                User userOriginal = getUserById(id);

                User userRequest = UFF.User;

                if (userOriginal.Email != userRequest.Email && busyEmail(userRequest.Email)) return _user = new UserResponse("Error, este Email ya está ocupado.", 404);

                if (userOriginal.Username != userRequest.Username && busyUsername(userRequest.Username)) return _user = new UserResponse("Error, este Username ya está ocupado.", 404);

                //Cargandolo.


                userOriginal.PathProfileImage = !string.IsNullOrEmpty(UFF.imageBase64) ?  (MultimediaRepository.base64Upload("Users",UFF.imageBase64,UFF.thumbnail) ? UFF.thumbnail : userOriginal.PathProfileImage) : userOriginal.PathProfileImage;
                userOriginal.Email = userRequest.Email;
                userOriginal.Name = userRequest.Name;
                userOriginal.Password = (!string.IsNullOrEmpty(userRequest.Password)) ? encryptrepo.Encrypting(userRequest.Password) : userOriginal.Password;
                userOriginal.RoleID = userRequest.RoleID; 
                userOriginal.Token = userRequest.Token;
                userOriginal.Username = userRequest.Username;

                db.Entry(userOriginal).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return _user = new UserResponse(String.Format("Correctamente Actualizado el usuario {0}",userOriginal.Name),1,null,true);

            }
            catch (Exception e)
            {

                return _user = new UserResponse(String.Format("Error en la petición -> {0}",e.Message));
            }
        }

        public UserResponse deleteUser(int id)
        {
            UserResponse _userRes;

            using (var trans =  db.Database.BeginTransaction())
            {

                try
                {
                    User _userToDelete = getUserById(id);


                    if (_userToDelete != null) _userRes = new UserResponse("No existe ningun User con ese ID", 2);


                    string name = _userToDelete.Name;
                    var list = db.UsersCourses.Where(x => x.UserID == _userToDelete.UserID).ToList();

                    if (list.Count() > 0)
                    {
                        //Borro todas las inscripciones.

                        foreach (var item in list)
                        {
                            db.UsersCourses.Remove(item);
                        }
                    }

                    db.Users.Remove(_userToDelete);
                    db.SaveChanges();

                    trans.Commit();
                    return _userRes = new UserResponse(String.Format("Eliminado correctamente el usuario {0}",name),1,null,true);

                }
                catch (Exception e)
                {
                    trans.Rollback();
                    return _userRes = new UserResponse(String.Format("Ocurrió un error con la petición -> {0}",e.Message));
                }
            }
        }
    }
}