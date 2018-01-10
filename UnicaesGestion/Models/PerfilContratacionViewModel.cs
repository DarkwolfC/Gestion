using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class PerfilContratacionViewModel
    {
        public RequisitoModel requisito { get; set; }
        public PerfilContratacionModel perfilContratacion { get ; set; }
        public List<TipoRequisitoModel> tipoResquisito { get; set; }
        public List<PrioridadRequisitoModel> prioridadRequisito { get; set; }
        public List<CriteriosModel> criterios { get; set; }

    }
}