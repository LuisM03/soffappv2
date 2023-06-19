using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace soffapp.Migrations
{
    /// <inheritdoc />
    public partial class new_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asociacion_producto",
                columns: table => new
                {
                    id_asociacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_detalle_insumo = table.Column<long>(type: "bigint", nullable: false),
                    id_producto = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__asociaci__086AFDE72F539724", x => x.id_asociacion);
                });

            migrationBuilder.CreateTable(
                name: "orden_venta",
                columns: table => new
                {
                    id_orden = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_venta = table.Column<long>(type: "bigint", nullable: false),
                    id_producto = table.Column<long>(type: "bigint", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(16,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orden_ve__DD5B8F339D3CED6A", x => x.id_orden);
                });

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    id_producto = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producto__FF341C0D7BD4FA41", x => x.id_producto);
                });

            migrationBuilder.CreateTable(
                name: "proveedor",
                columns: table => new
                {
                    id_proveedor = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    empresa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    direccion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime", nullable: false),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ciudad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__proveedo__8D3DFE28A7339043", x => x.id_proveedor);
                });

            migrationBuilder.CreateTable(
                name: "venta",
                columns: table => new
                {
                    id_venta = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_venta = table.Column<DateTime>(type: "datetime", nullable: false),
                    metodo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    total = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    tipo_venta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__venta__459533BFAEB70A59", x => x.id_venta);
                });

            migrationBuilder.CreateTable(
                name: "compra",
                columns: table => new
                {
                    id_compra = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_proveedor = table.Column<long>(type: "bigint", nullable: false),
                    fecha_compra = table.Column<DateTime>(type: "datetime", nullable: false),
                    total = table.Column<decimal>(type: "decimal(16,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__compra__C4BAA60485813B99", x => x.id_compra);
                    table.ForeignKey(
                        name: "FK__compra__id_prove__3E52440B",
                        column: x => x.id_proveedor,
                        principalTable: "proveedor",
                        principalColumn: "id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "insumo",
                columns: table => new
                {
                    id_insumo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_proveedor = table.Column<long>(type: "bigint", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fecha_caducidad = table.Column<DateTime>(type: "datetime", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    medida = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__insumo__D4F202B18C032D1E", x => x.id_insumo);
                    table.ForeignKey(
                        name: "FK__insumo__estado__3B75D760",
                        column: x => x.id_proveedor,
                        principalTable: "proveedor",
                        principalColumn: "id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "detalle_insumo",
                columns: table => new
                {
                    id_detalle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_insumo = table.Column<long>(type: "bigint", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    medida = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__detalle___4F1332DE5CDE7167", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK__detalle_i__estad__412EB0B6",
                        column: x => x.id_insumo,
                        principalTable: "insumo",
                        principalColumn: "id_insumo");
                });

            migrationBuilder.CreateTable(
                name: "orden_compra",
                columns: table => new
                {
                    id_orden = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_compra = table.Column<long>(type: "bigint", nullable: false),
                    id_insumo = table.Column<long>(type: "bigint", nullable: false),
                    cantidad = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(16,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orden_co__DD5B8F33D58ECB75", x => x.id_orden);
                    table.ForeignKey(
                        name: "FK__orden_com__id_in__44FF419A",
                        column: x => x.id_insumo,
                        principalTable: "insumo",
                        principalColumn: "id_insumo");
                    table.ForeignKey(
                        name: "FK__orden_com__total__440B1D61",
                        column: x => x.id_compra,
                        principalTable: "compra",
                        principalColumn: "id_compra");
                });

            migrationBuilder.CreateIndex(
                name: "IX_compra_id_proveedor",
                table: "compra",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_insumo_id_insumo",
                table: "detalle_insumo",
                column: "id_insumo");

            migrationBuilder.CreateIndex(
                name: "IX_insumo_id_proveedor",
                table: "insumo",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_orden_compra_id_compra",
                table: "orden_compra",
                column: "id_compra");

            migrationBuilder.CreateIndex(
                name: "IX_orden_compra_id_insumo",
                table: "orden_compra",
                column: "id_insumo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asociacion_producto");

            migrationBuilder.DropTable(
                name: "detalle_insumo");

            migrationBuilder.DropTable(
                name: "orden_compra");

            migrationBuilder.DropTable(
                name: "orden_venta");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "venta");

            migrationBuilder.DropTable(
                name: "insumo");

            migrationBuilder.DropTable(
                name: "compra");

            migrationBuilder.DropTable(
                name: "proveedor");
        }
    }
}
