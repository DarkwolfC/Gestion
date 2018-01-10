using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class RequisitoModel
    {
        public int id { get; set; }
        public string denominacion { get; set; }
        public Nullable<int> idTipoRequisito { get; set; }
        public Nullable<int> idcriterios { get; set; }
        public Nullable<int> idPrioridadRequisito { get; set; }
        public Nullable<int> idPerfilContratacion { get; set; }
    }
}