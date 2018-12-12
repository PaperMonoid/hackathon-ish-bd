using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class TH_acceso
    {
        public virtual int I_seq { get; set; }
        public virtual int ID_usuario { get; set; }
        public virtual DateTime Fecha_acceso { get; set; }
    }
}