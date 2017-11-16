using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.ClassesCourse
{
    public class ClassResponse
    {
        public ClassResponse() { }

        public ClassResponse(Class_Course _class = null, bool _status = false, string _message = "Error en la clase", int _codeState = 0, ICollection<Resource_class> _resources = null)
        {
            this._status = _status;
            this._message = _message;
            this._codeState = _codeState;
            this._resources = _resources;
            this._class = _class;
        }

        public bool _status { get; set; }
        public string _message { get; set; }
        public int _codeState { get; set; }
        public Class_Course _class { get; set; }
        public ICollection<Resource_class> _resources { get; set; }
    }

    public class ClassesResponse
    {
        //Default class.
        public ClassesResponse() { }

        //Class with Parametters
        public ClassesResponse(ICollection<Class_Course> classes, bool status = false, string  message = "Error en la clase", int codeState = 0) {
            this._status = status;
            this._message = message;
            this._codeState = codeState;
            this._classes = classes;
        }


        public bool _status { get; set; }
        public string _message { get; set; }
        public int _codeState { get; set; }
        public ICollection<Class_Course> _classes { get; set; }
    }
}