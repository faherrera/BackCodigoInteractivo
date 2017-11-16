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
                return _clresponse = new ClassesResponse(_classes,false,e.Message);
            }

        }

        public ClassResponse detailClass(int code)
        {
            ClassResponse _cr;
            Class_Course _cc;

            if (code == 0) return _cr = new ClassResponse(_cc = null, false,"No puede ser 0 el codigo");

            if (!busyClass(code))  return _cr = new ClassResponse(_cc = null, false, "No existe la clase con ese codigo", 2);

            _cc = ctx.Classes.Where(x=> x.CodeClass == code).FirstOrDefault();

            
            return _cr = new ClassResponse(_cc,true,"Clase traida correctamente",1,getAllResources(_cc.Class_CourseID));

        }

        public ClassResponse storeClass(Class_Course _classCourse)
        {
            ClassResponse _cr;

            if (_classCourse == null) return _cr = new ClassResponse(_classCourse,false,"No se cargó, no envió correctamente los datos");

            int code = _classCourse.Class_CourseID;

            if (ctx.Classes.Where(x => x.Class_CourseID == code).Any()) return _cr = new ClassResponse(_classCourse,false,"No puede haber una clase con el mismo codigo, por favor reemplazarlo");

            try
            {
                ctx.Classes.Add(_classCourse);
                ctx.SaveChanges();


            }
            catch (Exception e)
            {
                return _cr = new ClassResponse(_classCourse,false,e.Message);
                
            }

            return _cr = new ClassResponse(_classCourse,true,"Cargado correctamente la clase",1);
        }

        public ClassResponse putClass(int code, Class_Course _classModified) {

            ClassResponse _cr;

            if (code == 0) return _cr = new ClassResponse(_classModified, false,"Error en el codigo de la clase, revisar por favor");

            if (_classModified == null) return _cr = new ClassResponse(_classModified, false, "Error con la carga de datos, está vacio");

            if (!busyClass(code)) return _cr = new ClassResponse(_classModified, false, "No existe la clase con ese codigo");

            Class_Course _classOriginal = ctx.Classes.Where(x => x.CodeClass == code).FirstOrDefault();

            _classOriginal.CourseID = _classModified.CourseID;
            _classOriginal.PathVideo = _classModified.PathVideo;
            _classOriginal.TitleClass = _classModified.TitleClass;

            try
            {
                ctx.Entry(_classOriginal).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return _cr = new ClassResponse(_classModified, false,"Error en la carga, intente nuevamente en unos minutos  "+ e.Message ,0);

            }

            return _cr = new ClassResponse(_classOriginal, true,"Clase actualizada correctamente",1);



        }

        public ClassResponse deleteClass(int code)
        {
            ClassResponse _cr;
            Class_Course _cc = null;

            if (!busyClass(code)) return _cr = new ClassResponse(_cc,false,"No existe la clase con ese codigo");


            Class_Course _classToRemove = ctx.Classes.Where(x => x.CodeClass == code).First();

            if(hasChildren(_classToRemove.Class_CourseID)) { return _cr = new ClassResponse(_classToRemove, false, "Esta clase posee recursos anidados,no es posible eliminarla hasta no modificar-eliminar a sus hijos", 4); } 

            string name = _classToRemove.TitleClass;
                
            try
            {
                ctx.Classes.Remove(_classToRemove);
                ctx.SaveChanges();
            }
            catch (Exception e)
            {

                return _cr = new ClassResponse(_cc,false,"Ocurrió un error en la eliminación, por favor intente nuevamente" + e.Message);
            }

            return _cr = new ClassResponse(_cc,true,string.Format("Clase {0} eliminada correctamente",name),1);

        }


        /// <summary>
        /// This method asks if there is any class with that code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool busyClass(int code)
        {
            return ctx.Classes.Any(x => x.CodeClass == code);
        }

        public ICollection<Resource_class> getAllResources(int id)
        {
            ICollection<Resource_class> _resource = ctx.Resources.Where(x => x.Class_CourseID == id).ToList();

            return _resource; 
        }

        public bool hasChildren(int id)
        {
            return ctx.Resources.Any(x => x.Class_CourseID == id);
        }
    }
}