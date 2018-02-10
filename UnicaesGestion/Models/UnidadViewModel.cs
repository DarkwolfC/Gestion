using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class UnidadViewModel
    {
        public int id { get; set; }
        public Unidad unidad { get; set; }
        public List<Unidad> cmbUnidad { get; set; }     
        public List<FuncionUnidad> FuncionUnidades { get; set; }
    }
}