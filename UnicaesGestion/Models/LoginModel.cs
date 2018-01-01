using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class LoginModel
    {
        public String Usuario { get; set; }
        public String Password { get; set; }
        public Boolean Rememberme { get; set; }
    }
}