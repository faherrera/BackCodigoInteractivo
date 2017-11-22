using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Roles.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static BackCodigoInteractivo.ModelsNotMapped.Roles.Responses.Responses;

namespace BackCodigoInteractivo.Repositories
{
    public class RoleRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public ICollection<Role> getAllRoles()
        {
            return ctx.Roles.ToList();
        }

        public Role getRole(int id)
        {
            return ctx.Roles.Find(id);
        }

        public bool bussyTitle (String title)
        {
            return ctx.Roles.Any(x=> x.Title == title);
        }

        ///Peticiones
        ///

        public RolesResponse listRoles()
        {
            RolesResponse _rolesRes;

            try
            {
                if (getAllRoles().Count() <= 0) { return _rolesRes = new RolesResponse("No hay roles cargados aún", 2); }



                return _rolesRes = new RolesResponse("Devolviendo Correctamente el listado",1,getAllRoles(),true);
            }
            catch (Exception e)
            {

                return _rolesRes = new RolesResponse(String.Format("Error en la petición -> {0}", e.Message));

            }

           
        }

        public RoleResponse detailRole(int id)
        {
            RoleResponse _roleRes;

            try
            {
                if (getRole(id) == null) return _roleRes = new RoleResponse("No existe ningún rol con ese ID",2);

                return _roleRes = new RoleResponse("Peticion correcta",1,getRole(id),true);
            }
            catch (Exception e)
            {

                return _roleRes = new RoleResponse(String.Format("Error en la petición -> {0}",e.Message));
            }
        }

        public RoleResponse storeRole(Role role)
        {
            RoleResponse _roleRes;

            try
            {
                if (bussyTitle(role.Title)) return _roleRes = new RoleResponse(String.Format("El nombre {0} ya está ocupado, debe elegír otro",role.Title),2);

                ctx.Roles.Add(role);
                ctx.SaveChanges();

                return _roleRes = new RoleResponse(String.Format("Rol {0} cargado correctamente ",role.Title),1,role,true);
            }
            catch (Exception e)
            {
                return _roleRes = new RoleResponse(String.Format("Error en la peticion, -> {0}",e.Message));
            }
        }

        public RoleResponse putRole(int id,Role rolePut)
        {
            RoleResponse _roleRes;


            try
            {
                if (bussyTitle(rolePut.Title)) return _roleRes = new RoleResponse(String.Format("El nombre {0} ya está ocupado, debe elegír otro", rolePut.Title), 2);

                if (getRole(id) == null) return _roleRes = new RoleResponse("No existe Rol con ese ID",2);

                Role _roleModified = getRole(id);

                _roleModified.Title = rolePut.Title;

                ctx.Entry(_roleModified).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

                return _roleRes = new RoleResponse(String.Format("Rol {0} cargado correctamente ", _roleModified.Title), 1, _roleModified, true);
            }
            catch (Exception e)
            {
                return _roleRes = new RoleResponse(String.Format("Error en la peticion, -> {0}", e.Message));
            }
        }

        public RoleResponse deleteRole(int id)
        {
            RoleResponse _roleRes;

            try
            {
                if (getRole(id) == null) return _roleRes = new RoleResponse("No existe ningún rol con ese ID", 2);

                Role _roleToDelete = getRole(id);

                ctx.Roles.Remove(_roleToDelete);
                ctx.SaveChanges();

                return _roleRes = new RoleResponse("Peticion correcta, Rol eliminado", 1, null , true);
            }
            catch (Exception e)
            {

                return _roleRes = new RoleResponse(String.Format("Error en la petición -> {0}", e.Message));
            }
        }

    }
}