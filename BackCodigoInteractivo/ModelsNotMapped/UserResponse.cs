using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped
{
    public class UserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PathPhoto { get; set; }
        public string Token { get; set; }
    }
}