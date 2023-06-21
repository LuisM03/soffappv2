using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace soffapp.Models;

public partial class SoffDataContext : DbContext
{
    public SoffDataContext()
    {
    }

    public SoffDataContext(DbContextOptions<SoffDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsociacionProducto> AsociacionProductos { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetalleInsumo> DetalleInsumos { get; set; }

    public virtual DbSet<Insumo> Insumos { get; set; }

    public virtual DbSet<OrdenCompra> OrdenCompras { get; set; }

    public virtual DbSet<OrdenVentum> OrdenVenta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsociacionProducto>(entity =>
        {
            entity.HasKey(e => e.IdAsociacion).HasName("PK__asociaci__086AFDE72F539724");

            entity.ToTable("asociacion_producto");

            entity.Property(e => e.IdAsociacion).HasColumnName("id_asociacion");
            entity.Property(e => e.IdDetalleInsumo).HasColumnName("id_detalle_insumo");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdDetalleInsumoNavigation).WithMany(p => p.AsociacionProductos)
                .HasForeignKey(d => d.IdDetalleInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_asociacion_producto_detalle_insumo");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.AsociacionProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_asociacion_producto_producto");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__compra__C4BAA60485813B99");

            entity.ToTable("compra");

            entity.HasIndex(e => e.IdProveedor, "IX_compra_id_proveedor");

            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("datetime")
                .HasColumnName("fecha_compra");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__compra__id_prove__3E52440B");
        });

        modelBuilder.Entity<DetalleInsumo>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__detalle___4F1332DE5CDE7167");

            entity.ToTable("detalle_insumo");

            entity.HasIndex(e => e.IdInsumo, "IX_detalle_insumo_id_insumo");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");
            entity.Property(e => e.Medida)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medida");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.DetalleInsumos)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalle_insumo_insumo");
        });

        modelBuilder.Entity<Insumo>(entity =>
        {
            entity.HasKey(e => e.IdInsumo).HasName("PK__insumo__D4F202B18C032D1E");

            entity.ToTable("insumo");

            entity.HasIndex(e => e.IdProveedor, "IX_insumo_id_proveedor");

            entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCaducidad)
                .HasColumnType("datetime")
                .HasColumnName("fecha_caducidad");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Medida)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("medida");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Insumos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__insumo__estado__3B75D760");
        });

        modelBuilder.Entity<OrdenCompra>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__orden_co__DD5B8F33D58ECB75");

            entity.ToTable("orden_compra");

            entity.HasIndex(e => e.IdCompra, "IX_orden_compra_id_compra");

            entity.HasIndex(e => e.IdInsumo, "IX_orden_compra_id_insumo");

            entity.Property(e => e.IdOrden).HasColumnName("id_orden");
            entity.Property(e => e.Cantidad)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("cantidad");
            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.IdInsumo).HasColumnName("id_insumo");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orden_com__total__440B1D61");

            entity.HasOne(d => d.IdInsumoNavigation).WithMany(p => p.OrdenCompras)
                .HasForeignKey(d => d.IdInsumo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orden_com__id_in__44FF419A");
        });

        modelBuilder.Entity<OrdenVentum>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__orden_ve__DD5B8F339D3CED6A");

            entity.ToTable("orden_venta");

            entity.Property(e => e.IdOrden).HasColumnName("id_orden");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.OrdenVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orden_venta_producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.OrdenVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orden_venta_venta");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__producto__FF341C0D7BD4FA41");

            entity.ToTable("producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__proveedo__8D3DFE28A7339043");

            entity.ToTable("proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("empresa");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__venta__459533BFAEB70A59");

            entity.ToTable("venta");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.Metodo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metodo");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_venta");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
