﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}