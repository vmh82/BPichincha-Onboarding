
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
        public CreditoAutoDbContext(DbContextOptions<CreditoAutoDbContext> options) : base(options)
        {
        }
        public virtual DbSet<AsignacionCliente> AsignacionClientes { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Patio> Patios { get; set; } = null!;
        public virtual DbSet<Ejecutivo> Ejecutivos { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<AsignacionCliente>(entity =>
            {
                entity.HasKey(e => e.AsignacionId);

                entity.ToTable("AsignacionCliente");

                entity.Property(e => e.AsignacionId).HasColumnName("AsignacionId");

                entity.Property(e => e.FechaAsignacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FechaAsignacion");

                entity.Property(e => e.NumeroPuntoVenta).HasColumnName("NumeroPuntoVenta");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Identificacion");

                entity.HasOne(d => d.Patio)
                    .WithMany(p => p.AsignacionClientes)
                    .HasForeignKey(d => d.NumeroPuntoVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AsignacionCliente_Patio");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.AsignacionClientes)
                    .HasForeignKey(d => d.Identificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AsignacionCliente_Cliente");
            });

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

            modelBuilder.Entity<Patio>(entity =>
            {
                entity.HasKey(e => e.NumeroPuntoVenta)
                      .HasName("PK_NumeroPuntoVenta");

                entity.ToTable("Patio");
                entity.Property(e => e.NumeroPuntoVenta).HasColumnName("NumeroPuntoVenta");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Direccion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Telefono");
            });

            modelBuilder.Entity<Ejecutivo>(entity =>
            {
                entity.HasKey(e => e.Identificacion)
                    .HasName("PK_Ejecutivo");

                entity.ToTable("Ejecutivo");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Identificacion");

                entity.Property(e => e.Nombres)
                  .IsRequired()
                  .HasMaxLength(250)
                  .IsUnicode(false)
                  .HasColumnName("Nombres");

                entity.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Apellidos");

                entity.Property(e => e.Direccion)
                  .IsRequired()
                  .HasMaxLength(250)
                  .IsUnicode(false)
                  .HasColumnName("Direccion");

                entity.Property(e => e.TelefonoConvencional)
                   .IsRequired()
                   .HasMaxLength(250)
                   .IsUnicode(false)
                   .HasColumnName("TelefonoConvencional");

            
                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Celular");

                entity.Property(e => e.NumeroPuntoVenta).HasColumnName("NumeroPuntoVenta");

                entity.Property(e => e.Edad).HasColumnName("Edad");


                entity.HasOne(d => d.Patio)
                    .WithMany(p => p.Ejecutivos)
                    .HasForeignKey(d => d.NumeroPuntoVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ejecutivo_Patio");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.Placa)
                    .HasName("PK_Vehiculo");

                entity.ToTable("Vehiculo");

                entity.Property(e => e.Avaluo)
                    .HasColumnType("money")
                    .HasColumnName("Avaluo");

                entity.Property(e => e.Cilindraje).IsRequired().IsUnicode(false).HasColumnName("Cilindraje");

                entity.Property(e => e.MarcaId).HasColumnName("MarcaId");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Modelo");

                entity.Property(e => e.NumeroChasis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NumeroChasis");

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Placa");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tipo");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Marca");
            });
        }
    }
}
