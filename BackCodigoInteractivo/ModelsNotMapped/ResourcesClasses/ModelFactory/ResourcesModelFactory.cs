using BackCodigoInteractivo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.ResourcesClasses.ModelFactory
{
    public class ResourcesModelFactory
    {
        ClassesRepository crep = new ClassesRepository();
        public ResourcesModelFactory() { }

        public ResourcesModelFactory(int CodeResource,String Title,string External,int classCode) {

            this.CodeResource = CodeResource;
            TitleResource = Title;
            ExternalLink = External;
            Class_CourseID = classCode;
            TitleClass = (crep.getClass(classCode) != null) ? crep.getClass(classCode).TitleClass : null;
        }

        public int CodeResource { get; set; }
        public String TitleResource { get; set; }
        //        public String Description { get; set; }
        public String ExternalLink { get; set; }
        // public String PathResource { get; set; }
        public int Class_CourseID { get; set; }     //The ClassCourse. 
        public string TitleClass { get; set; }
    }
}