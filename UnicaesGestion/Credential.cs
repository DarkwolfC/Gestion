//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnicaesGestion
{
    using System;
    using System.Collections.Generic;
    
    public partial class Credential
    {
        public int idPersonal { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public bool enable { get; set; }
        public bool reset { get; set; }
        public int idRol { get; set; }
    
        public virtual Personal Personal { get; set; }
        public virtual Role Role { get; set; }
    }
}