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
    
    public partial class PerfilContratacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PerfilContratacion()
        {
            this.PuestoTrabajoes = new HashSet<PuestoTrabajo>();
            this.Requisitoes = new HashSet<Requisito>();
        }
    
        public int id { get; set; }
        public System.DateTime fechaElaboracion { get; set; }
        public string analista { get; set; }
        public string aprobadoPor { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuestoTrabajo> PuestoTrabajoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requisito> Requisitoes { get; set; }
    }
}