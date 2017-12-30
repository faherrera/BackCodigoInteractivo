using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.DAL
{
    public class CodigoInteractivoContext : DbContext
    {
        public System.Data.Entity.DbSet<BackCodigoInteractivo.Models.Course> Courses { get; set; }
        public DbSet<Class_Course> Classes { get; set; }
        public DbSet<Resource_class> Resources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Course> UsersCourses { get; set; }
    }
}