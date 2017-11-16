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

    }
}