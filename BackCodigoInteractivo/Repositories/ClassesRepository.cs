using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Repositories
{
    public class ClassesRepository
    {
        private CodigoInteractivoContext ctx = new CodigoInteractivoContext();

        public IQueryable<Class_Course> getClasses()
        {

            return ctx.Classes;
        }
    }
}