﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Entidades
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class Entities : DbContext
{
    public Entities()
        : base("name=Entities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Denuncias> Denuncias { get; set; }

    public virtual DbSet<DonacionesInsumos> DonacionesInsumos { get; set; }

    public virtual DbSet<DonacionesMonetarias> DonacionesMonetarias { get; set; }

    public virtual DbSet<MotivoDenuncia> MotivoDenuncia { get; set; }

    public virtual DbSet<Necesidades> Necesidades { get; set; }

    public virtual DbSet<NecesidadesDonacionesInsumos> NecesidadesDonacionesInsumos { get; set; }

    public virtual DbSet<NecesidadesDonacionesMonetarias> NecesidadesDonacionesMonetarias { get; set; }

    public virtual DbSet<NecesidadesReferencias> NecesidadesReferencias { get; set; }

    public virtual DbSet<NecesidadesValoraciones> NecesidadesValoraciones { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

}

}
