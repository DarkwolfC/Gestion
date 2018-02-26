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
       

        public UnidadViewModel()
        {
            unidad = new Unidad();
        }

        public List<Unidad> cmbUnidad { get; set; }
    }
}