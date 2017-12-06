using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.ClassesCourse.ModelFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.ClassesCourse
{
    public class ClassResponse
    {
        public ClassResponse() { }

        public ClassResponse(ClassesModelFactory _class = null, bool _status = false, string _message = "Error en la clase", int _codeState = 0)
        {
            this._status = _status;
            this._message = _message;
            this._codeState = _codeState;
            this._class = _class;
        }

        public bool _status { get; set; }
        public string _message { get; set; }
        public int _codeState { get; set; }
        public ClassesModelFactory _class { get; set; }
    }

    public class ClassesResponse
    {
        //Default class.
        public ClassesResponse() { }

        //Class with Parametters
        public ClassesResponse(ICollection<ClassesModelFactory> classes, bool status = false, string  message = "Error en la clase", int codeState = 0) {
            this._status = status;
            this._message = message;
            this._codeState = codeState;
            this._classes = classes;
        }


        public bool _status { get; set; }
        public string _message { get; set; }
        public int _codeState { get; set; }
        public ICollection<ClassesModelFactory> _classes { get; set; }
    }
}