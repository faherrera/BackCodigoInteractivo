using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped
{
    public class ResponseRepository
    {
        public ResponseRepository(String _message = " Error en la petición  ", int _codeState = 0, Object _data = null, bool _status = false)
        {
            this.status = _status;
            this.message = _message;
            this.codeState = _codeState;
            this.data = _data;

        }

        public bool status { get; set; }
        public string message { get; set; }
        public int codeState { get; set; }
        public Object data{ get; set; }
    }
}