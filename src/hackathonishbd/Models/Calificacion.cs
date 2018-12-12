using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class Calificacion
    {
        public virtual int IdCalificaciones { get; set; }
        public virtual int IdMaestro { get; set; }
        public virtual int IdAlumno { get; set; }
        public virtual int Valor { get; set; }
        public virtual bool Final { get; set; }
        public virtual DateTime FechaFinal { get; set; }
    }
}