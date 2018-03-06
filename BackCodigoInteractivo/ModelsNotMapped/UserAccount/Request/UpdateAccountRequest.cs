using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.UserAccount.Request
{
    public class UpdateAccountRequest
    {
        public string DNI { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ImageRequest ImageRequest { get; set; }

    }

    public class ImageRequest
    {
        public string Baase64Code { get; set; }
        public string PathProfileImage { get; set; }
    }
}