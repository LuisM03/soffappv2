﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using soffapp.Models;

#nullable disable

namespace soffapp.Migrations
{
    [DbContext(typeof(SoffDataContext))]
    partial class SoffDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("soffapp.Models.AsociacionProducto", b =>
                {
                    b.Property<long>("IdAsociacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_asociacion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdAsociacion"));

                    b.Property<long>("IdDetalleInsumo")
                        .HasColumnType("bigint")
                        .HasColumnName("id_detalle_insumo");

                    b.Property<long>("IdProducto")
                        .HasColumnType("bigint")
                        .HasColumnName("id_producto");

                    b.HasKey("IdAsociacion")
                        .HasName("PK__asociaci__086AFDE72F539724");

                    b.HasIndex("IdDetalleInsumo");

                    b.HasIndex("IdProducto");

                    b.ToTable("asociacion_producto", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.Compra", b =>
                {
                    b.Property<long>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_compra");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdCompra"));

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_compra");

                    b.Property<long>("IdProveedor")
                        .HasColumnType("bigint")
                        .HasColumnName("id_proveedor");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("total");

                    b.HasKey("IdCompra")
                        .HasName("PK__compra__C4BAA60485813B99");

                    b.HasIndex(new[] { "IdProveedor" }, "IX_compra_id_proveedor");

                    b.ToTable("compra", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.DetalleInsumo", b =>
                {
                    b.Property<long>("IdDetalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_detalle");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdDetalle"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("estado");

                    b.Property<long>("IdInsumo")
                        .HasColumnType("bigint")
                        .HasColumnName("id_insumo");

                    b.Property<string>("Medida")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("medida");

                    b.HasKey("IdDetalle")
                        .HasName("PK__detalle___4F1332DE5CDE7167");

                    b.HasIndex(new[] { "IdInsumo" }, "IX_detalle_insumo_id_insumo");

                    b.ToTable("detalle_insumo", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.Insumo", b =>
                {
                    b.Property<long>("IdInsumo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_insumo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdInsumo"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("estado");

                    b.Property<DateTime>("FechaCaducidad")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_caducidad");

                    b.Property<long>("IdProveedor")
                        .HasColumnType("bigint")
                        .HasColumnName("id_proveedor");

                    b.Property<string>("Medida")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("medida");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("precio");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.HasKey("IdInsumo")
                        .HasName("PK__insumo__D4F202B18C032D1E");

                    b.HasIndex(new[] { "IdProveedor" }, "IX_insumo_id_proveedor");

                    b.ToTable("insumo", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.OrdenCompra", b =>
                {
                    b.Property<long>("IdOrden")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_orden");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdOrden"));

                    b.Property<string>("Cantidad")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("cantidad")
                        .IsFixedLength();

                    b.Property<long>("IdCompra")
                        .HasColumnType("bigint")
                        .HasColumnName("id_compra");

                    b.Property<long>("IdInsumo")
                        .HasColumnType("bigint")
                        .HasColumnName("id_insumo");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("precio_unitario");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("total");

                    b.HasKey("IdOrden")
                        .HasName("PK__orden_co__DD5B8F33D58ECB75");

                    b.HasIndex(new[] { "IdCompra" }, "IX_orden_compra_id_compra");

                    b.HasIndex(new[] { "IdInsumo" }, "IX_orden_compra_id_insumo");

                    b.ToTable("orden_compra", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.OrdenVentum", b =>
                {
                    b.Property<long>("IdOrden")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_orden");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdOrden"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("cantidad");

                    b.Property<long>("IdProducto")
                        .HasColumnType("bigint")
                        .HasColumnName("id_producto");

                    b.Property<long>("IdVenta")
                        .HasColumnType("bigint")
                        .HasColumnName("id_venta");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("precio_unitario");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("total");

                    b.HasKey("IdOrden")
                        .HasName("PK__orden_ve__DD5B8F339D3CED6A");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdVenta");

                    b.ToTable("orden_venta", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.Producto", b =>
                {
                    b.Property<long>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_producto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdProducto"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("estado");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("precio");

                    b.HasKey("IdProducto")
                        .HasName("PK__producto__FF341C0D7BD4FA41");

                    b.ToTable("producto", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.Proveedor", b =>
                {
                    b.Property<long>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_proveedor");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdProveedor"));

                    b.Property<string>("Ciudad")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ciudad");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("correo");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("direccion");

                    b.Property<string>("Empresa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("empresa");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit")
                        .HasColumnName("estado");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_registro");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("telefono");

                    b.HasKey("IdProveedor")
                        .HasName("PK__proveedo__8D3DFE28A7339043");

                    b.ToTable("proveedor", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.Ventum", b =>
                {
                    b.Property<long>("IdVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_venta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("IdVenta"));

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime")
                        .HasColumnName("fecha_venta");

                    b.Property<string>("Metodo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("metodo");

                    b.Property<string>("TipoVenta")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tipo_venta");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(16, 2)")
                        .HasColumnName("total");

                    b.HasKey("IdVenta")
                        .HasName("PK__venta__459533BFAEB70A59");

                    b.ToTable("venta", (string)null);
                });

            modelBuilder.Entity("soffapp.Models.AsociacionProducto", b =>
                {
                    b.HasOne("soffapp.Models.DetalleInsumo", "IdDetalleInsumoNavigation")
                        .WithMany("AsociacionProductos")
                        .HasForeignKey("IdDetalleInsumo")
                        .IsRequired()
                        .HasConstraintName("FK_asociacion_producto_detalle_insumo");

                    b.HasOne("soffapp.Models.Producto", "IdProductoNavigation")
                        .WithMany("AsociacionProductos")
                        .HasForeignKey("IdProducto")
                        .IsRequired()
                        .HasConstraintName("FK_asociacion_producto_producto");

                    b.Navigation("IdDetalleInsumoNavigation");

                    b.Navigation("IdProductoNavigation");
                });

            modelBuilder.Entity("soffapp.Models.Compra", b =>
                {
                    b.HasOne("soffapp.Models.Proveedor", "IdProveedorNavigation")
                        .WithMany("Compras")
                        .HasForeignKey("IdProveedor")
                        .IsRequired()
                        .HasConstraintName("FK__compra__id_prove__3E52440B");

                    b.Navigation("IdProveedorNavigation");
                });

            modelBuilder.Entity("soffapp.Models.DetalleInsumo", b =>
                {
                    b.HasOne("soffapp.Models.Insumo", "IdInsumoNavigation")
                        .WithMany("DetalleInsumos")
                        .HasForeignKey("IdInsumo")
                        .IsRequired()
                        .HasConstraintName("FK_detalle_insumo_insumo");

                    b.Navigation("IdInsumoNavigation");
                });

            modelBuilder.Entity("soffapp.Models.Insumo", b =>
                {
                    b.HasOne("soffapp.Models.Proveedor", "IdProveedorNavigation")
                        .WithMany("Insumos")
                        .HasForeignKey("IdProveedor")
                        .IsRequired()
                        .HasConstraintName("FK__insumo__estado__3B75D760");

                    b.Navigation("IdProveedorNavigation");
                });

            modelBuilder.Entity("soffapp.Models.OrdenCompra", b =>
                {
                    b.HasOne("soffapp.Models.Compra", "IdCompraNavigation")
                        .WithMany("OrdenCompras")
                        .HasForeignKey("IdCompra")
                        .IsRequired()
                        .HasConstraintName("FK__orden_com__total__440B1D61");

                    b.HasOne("soffapp.Models.Insumo", "IdInsumoNavigation")
                        .WithMany("OrdenCompras")
                        .HasForeignKey("IdInsumo")
                        .IsRequired()
                        .HasConstraintName("FK__orden_com__id_in__44FF419A");

                    b.Navigation("IdCompraNavigation");

                    b.Navigation("IdInsumoNavigation");
                });

            modelBuilder.Entity("soffapp.Models.OrdenVentum", b =>
                {
                    b.HasOne("soffapp.Models.Producto", "IdProductoNavigation")
                        .WithMany("OrdenVenta")
                        .HasForeignKey("IdProducto")
                        .IsRequired()
                        .HasConstraintName("FK_orden_venta_producto");

                    b.HasOne("soffapp.Models.Ventum", "IdVentaNavigation")
                        .WithMany("OrdenVenta")
                        .HasForeignKey("IdVenta")
                        .IsRequired()
                        .HasConstraintName("FK_orden_venta_venta");

                    b.Navigation("IdProductoNavigation");

                    b.Navigation("IdVentaNavigation");
                });

            modelBuilder.Entity("soffapp.Models.Compra", b =>
                {
                    b.Navigation("OrdenCompras");
                });

            modelBuilder.Entity("soffapp.Models.DetalleInsumo", b =>
                {
                    b.Navigation("AsociacionProductos");
                });

            modelBuilder.Entity("soffapp.Models.Insumo", b =>
                {
                    b.Navigation("DetalleInsumos");

                    b.Navigation("OrdenCompras");
                });

            modelBuilder.Entity("soffapp.Models.Producto", b =>
                {
                    b.Navigation("AsociacionProductos");

                    b.Navigation("OrdenVenta");
                });

            modelBuilder.Entity("soffapp.Models.Proveedor", b =>
                {
                    b.Navigation("Compras");

                    b.Navigation("Insumos");
                });

            modelBuilder.Entity("soffapp.Models.Ventum", b =>
                {
                    b.Navigation("OrdenVenta");
                });
#pragma warning restore 612, 618
        }
    }
}
