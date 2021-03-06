﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string PathProfileImage { get; set; }
        public string DNI { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public bool Availability { get; set; }
        public int RoleID { get; set; }     //RolID
        public virtual Role Role { get; set; }

       
        public ICollection<User_Course> UserCourses { get; set; }
    }
}