using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class Usuario
    {
        public virtual int IdUsuario { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual string Clave { get; set; }
        public virtual char Sexo { get; set; }
        public virtual int Rol { get; set; }
        public virtual DateTime FechaRegistro { get; set; }
        public virtual DateTime FechaAcceso { get; set; }
        public virtual bool Activo { get; set; }
    }
}