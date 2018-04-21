using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }       
        public Personal personal { get; set; }
        public User user { get; set; }

        public UsuarioViewModel()
        {
            personal = new Personal();
            user = new User();

        }

        //dropdown
        public List<Personal> cmbpersonal { get; set; }
        public List<Role> cmbRol { get; set; }
        public List<PuestoTrabajo> cmbPuesto { get; set; }

    }
}