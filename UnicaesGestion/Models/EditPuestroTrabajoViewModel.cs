using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class EditPuestroTrabajoViewModel
    {
        public int Id { get; set; }
        public PuestoTrabajo puesto{ get; set; }

        public List<FuncionPuestoTrabajo> funciones{ get; set; }

        public List<Requisito> requisitos { get; set; }

        public TipoPuesto tipoPuesto { get; set; }

       
        public List<CompetenciaPuestoTrabajo> competencias { get; set; }

        public List<TipoPuesto>  cmbTipoPuesto { get; set; }

        public List<PuestoTrabajo> cmbJefeInmediato { get; set; }

        public List<PuestoTrabajo> cmbPuesto { get; set; }

        public List<Unidad> cmbUnidades { get; set; }

    }
}