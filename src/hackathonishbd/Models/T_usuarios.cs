using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class T_usuarios
    {
        public virtual int ID_usuario { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual string Clave { get; set; }
        public virtual char Sexo { get; set; }
        public virtual int Rol { get; set; }
        public virtual DateTime Fecha_registro { get; set; }
        public virtual DateTime Fecha_acceso { get; set; }
        public virtual bool B_activo { get; set; }
    }
}