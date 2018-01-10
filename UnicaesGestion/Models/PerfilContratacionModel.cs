using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class PerfilContratacionModel
    {
        public int id { get; set; }
        public System.DateTime fechaElaboracion { get; set; }
        public string analista { get; set; }
        public string aprobadoPor { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    }
}