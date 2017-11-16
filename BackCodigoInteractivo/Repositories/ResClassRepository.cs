using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.ResourcesClasses.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class ResClassRepository
    {
        CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public IQueryable<Resource_class> getAllResources()
        {
            return ctx.Resources;
        }

        public Resource_class getResource(int code)
        {
            Resource_class _rc = new Resource_class();

            return _rc = ctx.Resources.Where(x => x.CodeResource == code).First();
        }

        /// <summary>
        /// Para saber si está ocupado. En caso de estarlo es porque existe ya el registro.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>

        public bool busyResource(int code)
        {
            return ctx.Resources.Any(x=> x.CodeResource == code);
        }

        public ListResClassResponse listResources()
        {
            ListResClassResponse _lrcresponse;

            ICollection<Resource_class> _rcl;

            try
            {
                _rcl= getAllResources().ToList();

                if (_rcl.Count() > 0)
                {
                    return _lrcresponse = new ListResClassResponse(_rcl, true, "Listando toda las clases", 1);
                }
                else
                {
                    return _lrcresponse = new ListResClassResponse(_rcl, true, "No tiene Clases cargadas aún este curso", 2);
                }
            }
            catch (Exception e)
            {
                _rcl = null;
                return _lrcresponse = new ListResClassResponse(_rcl, false, e.Message);
            }

        }

        public ResClassesResponse storeResource(Resource_class _rclass) {

            ResClassesResponse _rcr;

            if(_rclass == null) { return _rcr = new ResClassesResponse(_rclass,false,"No puede estár nula la petición",0); }

            if (busyResource(_rclass.CodeResource)) return _rcr = new ResClassesResponse(_rclass,false,"El codigo ya está ocupado, no puede ser el mismo",2);

            try
            {
                ctx.Resources.Add(_rclass);
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return _rcr = new ResClassesResponse(_rclass,false,"Error en la carga de datos");
                
            }

            return _rcr = new ResClassesResponse(_rclass,true,"Recurso cargado correctamente",1);

        }

        public ResClassesResponse editResource(int code,Resource_class _rclass)
        {
            ResClassesResponse _rcr;

            if (!busyResource(code)) return _rcr = new ResClassesResponse(_rclass,false,"No existe ningun recurso con ese codigo, por favor revisarlo",2);
            
            if(_rclass == null) return _rcr = _rcr = new ResClassesResponse(_rclass, false, "La petición no puede ser nula");

            Resource_class _original = getResource(code);

            _original.Description = _rclass.Description;
            _original.ExternalLink = _rclass.ExternalLink;
            _original.TitleResource = _rclass.TitleResource;

            try
            {
                ctx.Entry(_original).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

            }
            catch (Exception e)
            {

                return _rcr = new ResClassesResponse(_rclass, false, "Ocurrío un error con la petición -> " + e.Message);

            }

            return _rcr = new ResClassesResponse(_original,true,"Recurso creado correctamente",1);
        }
        public ResClassesResponse detailResource(int code)
        {
            ResClassesResponse _rcr;
            Resource_class _rc;

            if (!busyResource(code)) return _rcr = new ResClassesResponse(_rc = null,false,"No existe el recurso con ese codigo");

            _rc = getResource(code);    //Asigno el recurso.

            return _rcr = new ResClassesResponse(_rc,true,"Recurso retornado con exito",1);

        }

        public ResClassesResponse deleteResource(int code) {

            ResClassesResponse _rcres;
            Resource_class _rc;

            if (!busyResource(code)) return _rcres = new ResClassesResponse(_rc = null,false,"No existe ningún recurso con este codigo");

            _rc = getResource(code);

            string name = _rc.TitleResource;
            try
            {
                ctx.Resources.Remove(_rc);
                ctx.SaveChanges();

            }
            catch (Exception e)
            {
                return _rcres = new ResClassesResponse(_rc,false,string.Format("Ocurrío un error durante la eliminacion de {0}, no pudo ser posible, intentelo de nuevo más tarde",name));

            }

            return _rcres = new ResClassesResponse(_rc = null,true,string.Format("Correctamente eliminado {0} ",name),1);


        }

        
    }
}