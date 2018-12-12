using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class Clave
    {
        public virtual int IdClave { get; set; }
        public virtual int IdUsuario { get; set; }
        public virtual string ClaveAnterior { get; set; }
        public virtual DateTime Fecha { get; set; }
    }
}