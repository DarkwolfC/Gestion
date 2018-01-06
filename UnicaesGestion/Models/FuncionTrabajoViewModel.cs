using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class FuncionTrabajoViewModel
    {

       
       
        public String funcionPuesto { get; set; }
        public int id { get; set; }
        public string titulo { get; set; }
        public string objetivo { get; set; }
        public Nullable<int> jefeInmediato { get; set; }
        public Nullable<int> idUnidad { get; set; }
        public Nullable<int> idPerfilContratacion { get; set; }
        public Nullable<int> idTipoPuesto { get; set; }



    }
}