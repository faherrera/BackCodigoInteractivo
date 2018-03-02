using BackCodigoInteractivo.DAL;
using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackCodigoInteractivo.ModelsNotMapped;
using BackCodigoInteractivo.ModelsNotMapped.ClassesCourse;
using BackCodigoInteractivo.ModelsNotMapped.ClassesCourse.ModelFactory;
using System.Web.Util;
using System.Data.Entity;

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

            List<ClassesModelFactory> _classes = new List<ClassesModelFactory>();


            try
            {
                var _cl = getAllClasses().ToList();

                foreach (var clase in _cl)
                {
                    ClassesModelFactory claseFactory = new ClassesModelFactory(clase.CodeClass,clase.TitleClass,clase.PathVideo,clase.Description,clase.CourseID);
                    _classes.Add(claseFactory);
                } 

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
                return _clresponse = new ClassesResponse(_classes,false,e.Message);
            }

        }

        public ClassResponse detailClass(int code)
        {
            ClassResponse _cr;
            Class_Course _cc;

            if (code == 0) return _cr = new ClassResponse(null, false,"No puede ser 0 el codigo");

            if (!busyClass(code))  return _cr = new ClassResponse(null, false, "No existe la clase con ese codigo", 2);

            _cc = ctx.Classes.Where(x=> x.CodeClass == code).FirstOrDefault();

            ClassesModelFactory modelFactory = new ClassesModelFactory(_cc.CodeClass,_cc.TitleClass,_cc.PathVideo,_cc.Description,_cc.CourseID);

            return _cr = new ClassResponse(modelFactory,true,"Clase traida correctamente",1);

        }

        public ClassResponse storeClass(Class_Course _classCourse)
        {
            ClassResponse _cr;

            if (_classCourse == null) return _cr = new ClassResponse(null,false,"No se cargó, no envió correctamente los datos");

            int code = _classCourse.CodeClass;

            if (ctx.Classes.Where(x => x.CodeClass == code).Any()) return _cr = new ClassResponse(null,false,"No puede haber una clase con el mismo codigo, por favor reemplazarlo");

            try
            {
                ctx.Classes.Add(_classCourse);
                ctx.SaveChanges();

                var model = new ClassesModelFactory(_classCourse.CodeClass,_classCourse.TitleClass,_classCourse.PathVideo,_classCourse.Description,_classCourse.CourseID);
                return _cr = new ClassResponse(model,true,"Cargado correctamente la clase",1);
            }
            catch (Exception e)
            {
                return _cr = new ClassResponse(null,false,e.Message);
                
            }

        }

        public ClassResponse putClass(int code, Class_Course _classModified) {

            ClassResponse _cr;

            if (code == 0) return _cr = new ClassResponse(null, false,"Error en el codigo de la clase, revisar por favor");

            if (_classModified == null) return _cr = new ClassResponse(null, false, "Error con la carga de datos, está vacio");

            if (!busyClass(code)) return _cr = new ClassResponse(null, false, "No existe la clase con ese codigo");

            Class_Course _classOriginal = ctx.Classes.Where(x => x.CodeClass == code).FirstOrDefault();

            _classOriginal.PathVideo = _classModified.PathVideo;
            _classOriginal.TitleClass = _classModified.TitleClass;
            _classOriginal.PathVideo = _classModified.PathVideo;
            _classOriginal.Description = _classModified.Description;
            _classOriginal.CourseID = _classModified.CourseID;  //ID del curso del cual depende.




            try
            {
                ctx.Entry(_classOriginal).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

                ClassesModelFactory _classModel = new ClassesModelFactory(_classOriginal.CodeClass,_classOriginal.TitleClass,_classOriginal.PathVideo,_classOriginal.Description,_classOriginal.CourseID);
                return _cr = new ClassResponse(_classModel, true, "Clase actualizada correctamente", 1);

            }
            catch (Exception e)
            {
                return _cr = new ClassResponse(null, false,"Error en la carga, intente nuevamente en unos minutos  "+ e.Message ,0);

            }




        }

        public ClassResponse deleteClass(int code)
        {
            ClassResponse _cr;

            if (!busyClass(code)) return _cr = new ClassResponse(null,false,"No existe la clase con ese codigo");


            Class_Course _classToRemove = ctx.Classes.Where(x => x.CodeClass == code).First();

            string name = _classToRemove.TitleClass;

            using (var tr = ctx.Database.BeginTransaction())
            {
                try
                {
                    var listResources = ctx.Resources.Where(x => x.Class_CourseID == _classToRemove.CodeClass).ToList();

                    foreach (var item in listResources)
                    {
                        ctx.Resources.Remove(item);
                        
                    }
                    ctx.Classes.Remove(_classToRemove);
                    ctx.SaveChanges();

                    tr.Commit();
                    return _cr = new ClassResponse(null, true, string.Format("Clase {0} eliminada correctamente", name), 1);
                }
                catch (Exception e)
                {
                    tr.Rollback();
                    return _cr = new ClassResponse(null, false, "Ocurrió un error en la eliminación, por favor intente nuevamente" + e.Message);
                }

            }


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

        public ClassesResponse getAllResourcesFromCourseCode(int code) {

            ClassesResponse classResponse;
            List<ClassesModelFactory> modelFactoryList = new List<ClassesModelFactory>();

            ClassesModelFactory modelFactory = null;

            var listClasses = ctx.Classes.Where(x => x.CourseID == code).ToList();

            foreach (var cla in listClasses)
            {
                modelFactory = new ClassesModelFactory(cla.CodeClass,cla.TitleClass,cla.PathVideo,cla.Description,cla.CourseID);

                modelFactoryList.Add(modelFactory);    
            }
           

            return classResponse = new ClassesResponse(modelFactoryList,true,"Traidos los datos",1);
        }
    }
}