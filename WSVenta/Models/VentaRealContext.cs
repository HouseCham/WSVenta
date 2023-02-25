using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WSVenta.Models;

public partial class VentaRealContext : DbContext
{
    public VentaRealContext()
    {
    }

    public VentaRealContext(DbContextOptions<VentaRealContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Concepto> Conceptos { get; set; }

    public virtual DbSet<Navbar> Navbars { get; set; }

    public virtual DbSet<NavbarSection> NavbarSections { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PFT6ASA\\MSSQLSERVER2;Database=VentaReal;Trusted_Connection=True;Encrypt=Yes;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cliente__3213E83F83560DC1");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.Email, "UQ__cliente__AB6E616474C565FE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApellidoM)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido_m");
            entity.Property(e => e.ApellidoP)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido_p");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Concepto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__concepto__3213E83FDFB6D838");

            entity.ToTable("concepto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Importe)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("importe");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("precioUnitario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Conceptos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__concepto__id_pro__412EB0B6");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Conceptos)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__concepto__id_pro__403A8C7D");
        });

        modelBuilder.Entity<Navbar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__navbar__3213E83F4E0CBFAD");

            entity.ToTable("navbar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Project)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("project");
        });

        modelBuilder.Entity<NavbarSection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__navbar_s__3213E83FE8DA10FA");

            entity.ToTable("navbar_section");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.NavbarId).HasColumnName("navbarId");
            entity.Property(e => e.SectionName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("sectionName");
            entity.Property(e => e.SectionUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sectionUrl");

            entity.HasOne(d => d.Navbar).WithMany(p => p.NavbarSections)
                .HasForeignKey(d => d.NavbarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__navbar_se__secti__5EBF139D");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83FFB8EE90E");

            entity.ToTable("producto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Costo)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("costo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("precioUnitario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F39D40D46");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(64)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PasswordKey)
                .HasMaxLength(128)
                .HasColumnName("passwordKey");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__venta__3213E83F9E9840E2");

            entity.ToTable("venta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__venta__id_client__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
