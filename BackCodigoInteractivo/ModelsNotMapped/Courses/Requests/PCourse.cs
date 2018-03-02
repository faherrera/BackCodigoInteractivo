using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.ModelsNotMapped.CoursesModels
{
   
    public class PCourse
    {
        public int code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string duration  { get; set; }
        public int typecourse { get; set; }
        public int mode { get; set; }
        public int level { get; set; }
        public string video_preview { get; set; }   //Url VideoEmbed
        public string thumbnail { get; set; }       //Nombre de la imagen
        public string imageBase64 { get; set; }     //Imagen base 64 con sus codigos.
        public int? professorId { get; set; }

        public bool availability { get; set; }
        public decimal price { get; set; }
        public string temary { get; set; }

        public DateTime startDate { get; set; }

        public DateTime finishDate { get; set; }
    }
}