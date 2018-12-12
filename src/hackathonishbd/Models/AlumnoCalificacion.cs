using System;
using NHibernate;

namespace hackathonishbd.Models
{
    public class AlumnoCalificacion
    {
        public Usuario Alumno { get; set; }
        public Calificacion Calificacion { get; set; }
    }
}
