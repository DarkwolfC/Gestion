using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class FuncionUnidadViewModel
    {

        public String unidadFunciones { get; set; }
        public int id { get; set; }
        public string nombre { get; set; }
        public string objetivo { get; set; }
        public Nullable<int> depende { get; set; }
        public Nullable<int> idPuestoResponsableTrabajo { get; set; }


    }
}