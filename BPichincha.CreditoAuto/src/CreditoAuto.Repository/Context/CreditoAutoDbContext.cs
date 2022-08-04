
using CreditoAuto.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Repository.Context
{
    public class CreditoAutoDbContext : DbContext
    {
        public CreditoAutoDbContext()
        {
        }

        public CreditoAutoDbContext(DbContextOptions<CreditoAutoDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.MarcaId);

                entity.ToTable("Marca");

                entity.Property(e => e.MarcaId).HasColumnName("MarcaId");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Descripcion");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Identificacion)
                    .HasName("PK_Identificacion");

                entity.ToTable("Cliente");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Identificacion");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Apellidos");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Direccion");

                entity.Property(e => e.Edad).HasColumnName("Edad");

                entity.Property(e => e.EstadoCivil).HasColumnName("EstadoCivil");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("FechaNacimiento");

                entity.Property(e => e.IdentificacionConyugue)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IdentificacionConyugue");

                entity.Property(e => e.NombreConyugue)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NombreConyugue");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombres");

                entity.Property(e => e.SujetoCredito)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                .HasColumnName("SujetoCredito");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Telefono");
            });

        }
        public virtual DbSet<Patio> PatioAuto { get; set; } = null!;
        public virtual DbSet<Cliente> Cliente { get; set; } = null!;
        public virtual DbSet<Marca> Marca { get; set; } = null!;
        public virtual DbSet<Ejecutivo> Ejecutivo { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculo { get; set; } = null!;
    }
}
