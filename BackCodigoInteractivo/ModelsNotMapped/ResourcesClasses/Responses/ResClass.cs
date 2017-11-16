using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.ResourcesClasses.Responses
{
    public class ResClassesResponse
    {
        public ResClassesResponse() { }

        public ResClassesResponse(Resource_class _rclass, bool _status = false, string _message = "Error en la clase", int _codeState = 0)
        {
            this._status = _status;
            this._message = _message;
            this._codeState = _codeState;
            this._rclass = _rclass;
        }

        public bool _status { get; set; }
        public string _message { get; set; }
        public int _codeState { get; set; }
        public Resource_class _rclass { get; set; }
    }

    public class ListResClassResponse
    {
        public ListResClassResponse() { }

        public ListResClassResponse(ICollection<Resource_class> _rclasses, bool _status = false, string _message = "Error en la clase", int _codeState = 0)
        {
            this._status = _status;
            this._message = _message;
            this._codeState = _codeState;
            this._rclasses = _rclasses;
        }

        public bool _status { get; set; }
        public string _message { get; set; }
        public int _codeState { get; set; }
        public ICollection<Resource_class> _rclasses { get; set; }
    }
}