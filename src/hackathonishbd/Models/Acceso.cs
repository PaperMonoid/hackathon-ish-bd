using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class Acceso
    {
        public virtual int IdAcceso { get; set; }
        public virtual int IdUsuario { get; set; }
        public virtual DateTime FechaAcceso { get; set; }
    }
}