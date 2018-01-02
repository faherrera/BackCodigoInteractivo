using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Users.ModelFactory
{
    

    public class UserModelFactory
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public UserModelFactory()
        {

        }

        public UserModelFactory(int UserID,string dni,string name, string email,string username)
        {
            MyCourse my = new MyCourse();

            DNI = dni;
            Name = name;
            Email = email;
            Username = username;
            Courses = my.getAllCourses(UserID);
        }

        public string DNI { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<MyCourse> Courses { get; set; }
    }

}