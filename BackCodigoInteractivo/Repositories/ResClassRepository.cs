using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.ResourcesClasses.ModelFactory;
using BackCodigoInteractivo.ModelsNotMapped.ResourcesClasses.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class ResClassRepository
    {
        ResourcesModelFactory resourceModelFactory = null;
        ICollection<ResourcesModelFactory> ListResourceModelFactory = null;
        CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public IQueryable<Resource_class> getAllResources()
        {
            return ctx.Resources;
        }

        public ResourcesModelFactory getResource(int code)
        {
            Resource_class _rc = _rc = ctx.Resources.Where(x => x.CodeResource == code).First();
            return resourceModelFactory = new ResourcesModelFactory(_rc.CodeResource,_rc.TitleResource,_rc.ExternalLink,_rc.Class_CourseID);
            

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
            ListResClassResponse _lrcresponse = null;

            try
            {
                List<Resource_class> listResoruce = getAllResources().ToList();

                if (listResoruce.Count() <= 0)
                {
                    return _lrcresponse = new ListResClassResponse(ListResourceModelFactory, false," No posee elementos cargados",3);
                }

                List<ResourcesModelFactory> ListFactory = new List<ResourcesModelFactory>();
                foreach (var res in listResoruce)
                {
                    resourceModelFactory = new ResourcesModelFactory(res.CodeResource,res.TitleResource,res.ExternalLink,res.Class_CourseID);

                    ListFactory.Add(resourceModelFactory);
                }

                return _lrcresponse = new ListResClassResponse(ListFactory, true, "Correctamente devueltos", 1);

            }
            catch (Exception e)
            {

                return _lrcresponse = new ListResClassResponse(null, false, "Ocurrío un error "+e.Message, 0);
            }
            

        }

        public ResClassesResponse storeResource(Resource_class _rclass) {

            ResClassesResponse _rcr;

            if(_rclass == null) { return _rcr = new ResClassesResponse(null,false,"No puede estár nula la petición",0); }

            if (busyResource(_rclass.CodeResource)) return _rcr = new ResClassesResponse(null,false,"El codigo ya está ocupado, no puede ser el mismo",2);

            try
            {
                ctx.Resources.Add(_rclass);
                ctx.SaveChanges();

                resourceModelFactory = new ResourcesModelFactory(_rclass.CodeResource, _rclass.TitleResource, _rclass.ExternalLink, _rclass.Class_CourseID); 
                return _rcr = new ResClassesResponse(resourceModelFactory, true,"Recurso cargado correctamente",1);
            }
            catch (Exception e)
            {
                return _rcr = new ResClassesResponse(null,false,string.Format("Error en la carga de datos ${0}",e.Message));
                
            }


        }

        public ResClassesResponse editResource(int code,Resource_class _rclass)
        {
            ResClassesResponse _rcr;

            if (!busyResource(code)) return _rcr = new ResClassesResponse(null,false,"No existe ningun recurso con ese codigo, por favor revisarlo",2);
            
            if(_rclass == null) return _rcr = _rcr = new ResClassesResponse(null, false, "La petición no puede ser nula");

            Resource_class _original = ctx.Resources.Where(x => x.CodeResource == code).First();

            _original.ExternalLink = _rclass.ExternalLink;
            _original.TitleResource = _rclass.TitleResource;
            _original.Class_CourseID = _rclass.Class_CourseID;

            try
            {
                ctx.Entry(_original).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

                resourceModelFactory = new ResourcesModelFactory(_original.CodeResource, _original.TitleResource, _original.ExternalLink, _original.Class_CourseID);
                return _rcr = new ResClassesResponse(resourceModelFactory, true, "Recurso cargado correctamente", 1);
            }
            catch (Exception e)
            {

                return _rcr = new ResClassesResponse(null, false, "Ocurrío un error con la petición -> " + e.Message);

            }

        }
        public ResClassesResponse detailResource(int code)
        {
            ResClassesResponse _rcr;

            if (!busyResource(code)) return _rcr = new ResClassesResponse( null,false,"No existe el recurso con ese codigo");

            resourceModelFactory = getResource(code);    //Asigno el recurso.

            return _rcr = new ResClassesResponse(resourceModelFactory,true,"Recurso retornado con exito",1);

        }

        public ResClassesResponse deleteResource(int code) {

            ResClassesResponse _rcres;
            Resource_class _rc;

            if (!busyResource(code)) return _rcres = new ResClassesResponse(null,false,"No existe ningún recurso con este codigo");

            _rc = ctx.Resources.First(x=> x.CodeResource == code);

            string name = _rc.TitleResource;

            try
            {
                ctx.Resources.Remove(_rc);
                ctx.SaveChanges();

                return _rcres = new ResClassesResponse(null,true,string.Format("Correctamente eliminado {0} ",name),1);

            }
            catch (Exception e)
            {
                return _rcres = new ResClassesResponse(null,false,string.Format("Ocurrío un error durante la eliminacion de {0}, no pudo ser posible, intentelo de nuevo más tarde. \n Error -> {1}",name,e.Message));

            }



        }

        
    }
}