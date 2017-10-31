using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsFactory
{
    public class RoleModelFactory
    {
        public Role Create(Role role)
        {
            return new Role
            {
                RoleID = role.RoleID,
                Title = role.Title
            };
        }
    }
}