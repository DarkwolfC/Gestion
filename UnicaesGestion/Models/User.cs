using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicaesGestion.Models
{
    public class User
    {
        public string Guid { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
        public bool Active { get; set; }
    }
}