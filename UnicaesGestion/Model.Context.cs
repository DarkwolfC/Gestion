﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GestionEntities : DbContext
    {
        public GestionEntities()
            : base("name=GestionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Criterio> Criterios { get; set; }
        public virtual DbSet<FuncionPuestoTrabajo> FuncionPuestoTrabajoes { get; set; }
        public virtual DbSet<FuncionUnidad> FuncionUnidads { get; set; }
        public virtual DbSet<Paso> Pasoes { get; set; }
        public virtual DbSet<Personal> Personals { get; set; }
        public virtual DbSet<Procedimiento> Procedimientoes { get; set; }
        public virtual DbSet<PuestoTrabajo> PuestoTrabajoes { get; set; }
        public virtual DbSet<TipoPaso> TipoPasoes { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<CatalogoCompetencia> CatalogoCompetencias { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<CompetenciaPuestoTrabajo> CompetenciaPuestoTrabajoes { get; set; }
        public virtual DbSet<TipoPuesto> TipoPuestoes { get; set; }
        public virtual DbSet<Requisito> Requisitoes { get; set; }
        public virtual DbSet<Unidad> Unidads { get; set; }
    }
}
