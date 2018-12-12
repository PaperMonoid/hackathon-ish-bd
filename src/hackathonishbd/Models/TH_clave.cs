using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hackathonishbd.Models
{
    public class TH_clave
    {
        public virtual int I_seq { get; set; }
        public virtual int ID_usuario { get; set; }
        public virtual string Clave_ant { get; set; }
        public virtual DateTime Fecha { get; set; }
    }
}