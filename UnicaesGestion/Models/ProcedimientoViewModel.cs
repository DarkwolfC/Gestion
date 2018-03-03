using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class ProcedimientoViewModel
    {
        public int id { get; set; }
        public Procedimiento procedimiento { get; set; }
        public Paso paso { get; set; }

        public ProcedimientoViewModel()
        {
            procedimiento = new Procedimiento();
            paso = new Paso();
        }   
        
        
       //dropdown
        public List<PuestoTrabajo> cmbPuestos { get; set; }
        public List<TipoPaso> cmbtipoPasos { get; set; }

      
    }
}