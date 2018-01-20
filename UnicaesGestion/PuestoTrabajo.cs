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
    
    public partial class PuestoTrabajo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PuestoTrabajo()
        {
            this.FuncionPuestoTrabajoes = new HashSet<FuncionPuestoTrabajo>();
            this.Pasoes = new HashSet<Paso>();
            this.Personals = new HashSet<Personal>();
            this.PuestoTrabajo1 = new HashSet<PuestoTrabajo>();
            this.CompetenciaPuestoTrabajoes = new HashSet<CompetenciaPuestoTrabajo>();
            this.Requisitoes = new HashSet<Requisito>();
        }
    
        public int id { get; set; }
        public string titulo { get; set; }
        public string objetivo { get; set; }
        public Nullable<int> jefeInmediato { get; set; }
        public int idUnidad { get; set; }
        public Nullable<int> idTipoPuesto { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public bool activo { get; set; }
        public bool aprobado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FuncionPuestoTrabajo> FuncionPuestoTrabajoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paso> Pasoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personal> Personals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuestoTrabajo> PuestoTrabajo1 { get; set; }
        public virtual PuestoTrabajo PuestoTrabajo2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompetenciaPuestoTrabajo> CompetenciaPuestoTrabajoes { get; set; }
        public virtual TipoPuesto TipoPuesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requisito> Requisitoes { get; set; }
        public virtual Unidad Unidad { get; set; }
    }
}
