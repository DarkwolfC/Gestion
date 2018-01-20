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
    
    public partial class Unidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unidad()
        {
            this.FuncionUnidads = new HashSet<FuncionUnidad>();
            this.PuestoTrabajoes = new HashSet<PuestoTrabajo>();
            this.Unidad1 = new HashSet<Unidad>();
            this.Procedimientoes = new HashSet<Procedimiento>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string objetivo { get; set; }
        public Nullable<int> depende { get; set; }
        public int idPuestoResponsableTrabajo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FuncionUnidad> FuncionUnidads { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuestoTrabajo> PuestoTrabajoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unidad> Unidad1 { get; set; }
        public virtual Unidad Unidad2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Procedimiento> Procedimientoes { get; set; }
    }
}
