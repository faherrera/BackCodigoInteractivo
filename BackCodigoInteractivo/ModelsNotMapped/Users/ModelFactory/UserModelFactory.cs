using BackCodigoInteractivo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Users.ModelFactory
{
    public class UserModelFactory
    {
        //private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public UserModelFactory(int UserID, string user, string Name, string Email, string Path, string Role,string dni,int roleId,ICollection<CourseBelongToModelFactory> courses)
        {
            this.UserID = UserID;
            this.Name = Name;
            Username = user;
            this.Email = Email;
            PathProfileImage = Path;
            this.Role = Role;
            this.DNI = dni;
            this.RoleID = roleId;
            Courses = courses;
        }
        public int UserID { get; set; }
        public string DNI { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PathProfileImage { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public int RoleID { get; set; }
        
        public ICollection<CourseBelongToModelFactory> Courses { get; set; }
    }

    public class CourseBelongToModelFactory
    {
        public int CourseCode { get; set; }
        public string Name { get; set; }
        public bool IsProfessor { get; set; }
        public bool Access { get; set; }
    }

}