
namespace DAO.Context
{
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System;
    using System.Data.Entity.Infrastructure;
    using Entidades;

    public partial class TpDBContext : DbContext
    {
        public TpDBContext()
            : base("name=EntitiesTP")
        {
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MotivoDenuncia>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<MotivoDenuncia>()
                .HasMany(e => e.Denuncias)
                .WithRequired(e => e.MotivoDenuncia)
                .HasForeignKey(e => e.IdMotivo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Necesidades>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Necesidades>()
                .Property(e => e.Valoracion)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Necesidades>()
                .HasMany(e => e.Denuncias)
                .WithRequired(e => e.Necesidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Necesidades>()
                .HasMany(e => e.NecesidadesDonacionesInsumos)
                .WithRequired(e => e.Necesidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Necesidades>()
                .HasMany(e => e.NecesidadesDonacionesMonetarias)
                .WithRequired(e => e.Necesidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Necesidades>()
                .HasMany(e => e.NecesidadesReferencias)
                .WithRequired(e => e.Necesidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Necesidades>()
                .HasMany(e => e.NecesidadesValoraciones)
                .WithRequired(e => e.Necesidades)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NecesidadesDonacionesInsumos>()
                .HasMany(e => e.DonacionesInsumos)
                .WithRequired(e => e.NecesidadesDonacionesInsumos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NecesidadesDonacionesMonetarias>()
                .HasMany(e => e.DonacionesMonetarias)
                .WithRequired(e => e.NecesidadesDonacionesMonetarias)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Denuncias)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.DonacionesInsumos)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.DonacionesMonetarias)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Necesidades)
                .WithRequired(e => e.Usuarios)
                .HasForeignKey(e => e.IdUsuarioCreador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.NecesidadesValoraciones)
                .WithRequired(e => e.Usuarios)
                .WillCascadeOnDelete(false);
        }
    }
}



