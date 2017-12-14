using BackCodigoInteractivo.Models;
using BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses;
using BackCodigoInteractivo.ModelsNotMapped.ResourcesClasses.ModelFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.ResourcesClasses.Responses
{
    public class ResClassesResponse : BaseResponse
    {
        public ResClassesResponse() { }

        public ResClassesResponse(ResourcesModelFactory _rclass, bool _status = false, string _message = "Error en la clase", int _codeState = 0)
        {
            this.status = _status;
            this.message = _message;
            this.codeState = _codeState;
            this.data = _rclass;
        }

    }

    public class ListResClassResponse : BaseResponses
    {
        public ListResClassResponse() { }

        public ListResClassResponse(ICollection<ResourcesModelFactory> _rclasses, bool _status = false, string _message = "Error en la clase", int _codeState = 0)
        {
            this.status = _status;
            this.message = _message;
            this.codeState = _codeState;
            this.data = _rclasses;
        }

       
        public ICollection<ResourcesModelFactory> data { get; set; }
    }
}