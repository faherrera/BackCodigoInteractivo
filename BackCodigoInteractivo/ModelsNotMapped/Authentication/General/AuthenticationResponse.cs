using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Authentication.General
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(bool status = false,string token = null)
        {
            this.status = status;
            this.token = token;
        }
        public bool status { get; set; }
        public string token { get; set; }
    }
}