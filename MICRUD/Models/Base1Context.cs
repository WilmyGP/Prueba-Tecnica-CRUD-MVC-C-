using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MICRUD.Models;

namespace MICRUD.Models;

public partial class Base1Context : DbContext
{

    public Base1Context()
    {
    }

    public Base1Context(DbContextOptions<Base1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Servicios> Servicio { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventar__3214EC0778A37A03");

            entity.ToTable("Inventario");

            entity.Property(e => e.FechaEntrada).HasColumnType("date");
            entity.Property(e => e.FechaSalida).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC070FD0FF7B");

            entity.ToTable("Producto");

            entity.Property(e => e.FechaExpiracion).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3214EC07CF9DE75C");

            entity.ToTable("Servicios");

            entity.Property(e => e.FechaEntrada).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<MICRUD.Models.Servicios>? Servicios { get; set; }
}
