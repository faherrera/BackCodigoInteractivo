using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.ModelsNotMapped.ClassesCourse;

namespace BackCodigoInteractivo.Repositories
{
    public class ClassesRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public IQueryable<Class_Course> getAllClasses()
        {

            return ctx.Classes;
        }

        public Class_Course getClass(int code)
        {
            return ctx.Classes.Where(x => x.CodeClass == code).FirstOrDefault();
        }

        public ClassesResponse listClasses()
        {
            ClassesResponse _clresponse;

            ICollection<Class_Course> _classes;

            try
            {
                _classes = getAllClasses().ToList(); 

                if (_classes.Count() > 0)
                {
                    return _clresponse = new ClassesResponse(_classes,true, "Listando toda las clases",1);
                }else
                {
                    return _clresponse = new ClassesResponse(_classes,true, "No tiene Clases cargadas aún este curso", 2 );
                }
            }
            catch (Exception e)
            {
                _classes = null;
                return _clresponse = new ClassesResponse(_classes);
            }

        }
    }
}