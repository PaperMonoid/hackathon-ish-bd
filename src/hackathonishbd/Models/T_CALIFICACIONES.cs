using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class T_CALIFICACIONES
    {
        public virtual int ID_calificaciones { get; set; }
        public virtual int ID_maestro { get; set; }
        public virtual int ID_alumno { get; set; }
        public virtual int Calificacion { get; set; }
        public virtual bool B_Final { get; set; }
        public virtual DateTime Fecha_Final { get; set; }
    }
}