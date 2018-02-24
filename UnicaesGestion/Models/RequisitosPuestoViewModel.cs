using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class RequisitosPuestoViewModel
    {
        public int IdRequisito { get; set; }
        public string Descripcion { get; set; }
        public int? IdCategoria { get; set; }
        public string Categoria { get; set; }
    }
}