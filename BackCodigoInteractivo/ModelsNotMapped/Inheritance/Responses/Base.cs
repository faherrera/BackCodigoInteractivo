using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Inheritance.Responses
{
    public class BaseResponses
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int codeState { get; set; }
    }

    public class BaseResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int codeState { get; set; }
        public Object data { get; set; }
    }
}