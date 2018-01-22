using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Users.ModelFactory;
using BackCodigoInteractivo.ModelsNotMapped.Users.Responses;
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
                    _userModelFactory = new UserModelFactory(item.UserID, item.Username, item.Name, item.Email, item.PathProfileImage, item.Role.Title);


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

                return _userRes = new UserResponse("User traido correctamente",1, _userModelFactory = new UserModelFactory(_us.UserID,_us.Username,_us.Name,_us.Email,_us.PathProfileImage,_us.Role.Title),true);
            }
            catch (Exception e)
            {
                return _userRes = new UserResponse("Error en la peticion, intente nuevamente por favor. -> " + e.Message);
            }
        }

        public UserResponse storeUser(User user)
        {
            UserResponse _userRes;

            try
            {
                if (user == null) return _userRes = new UserResponse("Error, debe enviar los datos.");

                if (busyEmail(user.Email)) return _userRes = new UserResponse("Error, este Email ya está ocupado.");

                if (busyUsername(user.Username)) return _userRes = new UserResponse("Error, este Username ya está ocupado.");

                db.Users.Add(user);
                db.SaveChanges();

                return _userRes = new UserResponse("Cargado correctamente",1,null,true);

            }
            catch (Exception e)
            {

                return _userRes = new UserResponse("Error en la peticion, intente nuevamente por favor. -> " +e.Message );
            }


        }
            
        public UserResponse putUser(int id, User user)
        {
            UserResponse _user;

            try
            {
                if(user == null) { return _user = new UserResponse("No puede estár vacia la petición",2); }

                if (busyEmail(user.Email)) return _user = new UserResponse("Error, este Email ya está ocupado.");

                if (busyUsername(user.Username)) return _user = new UserResponse("Error, este Username ya está ocupado.");


                if (getUserById(id) == null) { return _user = new UserResponse("No existe ningún Usuario con ese ID",2); }

                //Cargandolo.

                User _userModified = getUserById(id);

                _userModified.Email = user.Email;
                _userModified.Name = user.Name;
                _userModified.Password = user.Password;
                _userModified.RoleID = user.RoleID;
                _userModified.Token = user.Token;
                _userModified.Username = user.Username;

                db.Entry(_userModified).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return _user = new UserResponse(String.Format("Correctamente Actualizado el usuario {0}",_userModified.Name),1,null,true);

            }
            catch (Exception e)
            {

                return _user = new UserResponse(String.Format("Error en la petición -> {0}",e.Message));
            }
        }

        public UserResponse deleteUser(int id)
        {
            UserResponse _userRes;

            try
            {
                User _userToDelete = getUserById(id);


                if (_userToDelete != null) _userRes = new UserResponse("No existe ningun User con ese ID", 2);


                string name = _userToDelete.Name;

                db.Users.Remove(_userToDelete);
                db.SaveChanges();

                return _userRes = new UserResponse(String.Format("Eliminado correctamente el usuario {0}",name),1,null,true);

            }
            catch (Exception e)
            {

                return _userRes = new UserResponse(String.Format("Ocurrió un error con la petición -> {0}",e.Message));
            }
        }
    }
}