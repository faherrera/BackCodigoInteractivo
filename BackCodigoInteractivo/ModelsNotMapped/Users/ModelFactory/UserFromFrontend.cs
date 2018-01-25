using BackCodigoInteractivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.Users.ModelFactory
{
    public class UserFromFrontend
    {
        public User User { get; set; }
        public string thumbnail { get; set; }       //Nombre de la imagen
        public string imageBase64 { get; set; }     //Imagen base 64 con sus codigos.
    }
}