using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class PuestoTrabajoViewModel
    {
        public int Id { get; set; } 
        public PuestoTrabajo puesto{ get; set; }

        public PuestoTrabajoViewModel()
        {
            puesto = new PuestoTrabajo();
            
        }
        
        /* Dropdowns de la vista */
        public List<TipoPuesto>  cmbTipoPuesto { get; set; }

        public List<PuestoTrabajo> cmbJefeInmediato { get; set; }

        public List<PuestoTrabajo> cmbPuesto { get; set; }

        public List<Unidad> cmbUnidades { get; set; }

        public List<Categoria> cmbCategoria { get; set; }

        public List<CatalogoCompetencia> cmbCatalogoCompetencia { get; set; }

    }
}