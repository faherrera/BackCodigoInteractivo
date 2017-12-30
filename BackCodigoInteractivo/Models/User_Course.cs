using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BackCodigoInteractivo.Models
{
    public class User_Course
    {
        [Key]
        public int UserCourseID { get; set; }
        public int UserID { get; set; } //Con el que está conectado.
        public int CourseID { get; set; } //Al curso que pertenece. Es CODE porque es el que tengo en el frontend
        public bool Access { get; set; } //Si tiene acceso o no.
        public string pathCertificate { get; set; } //En caso de tener certificado se verá aquí el PATH.
        public bool isInstructor { get; set; }  //Si es instructor debe ser True. Esto le dará la posibilidad de subir y modificar el curso.

    }
}