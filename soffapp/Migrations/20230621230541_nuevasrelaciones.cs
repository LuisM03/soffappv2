using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace soffapp.Migrations
{
    /// <inheritdoc /> 
    public partial class nuevasrelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__detalle_i__estad__412EB0B6",
                table: "detalle_insumo");

            migrationBuilder.CreateIndex(
                name: "IX_orden_venta_id_producto",
                table: "orden_venta",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_orden_venta_id_venta",
                table: "orden_venta",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_asociacion_producto_id_detalle_insumo",
                table: "asociacion_producto",
                column: "id_detalle_insumo");

            migrationBuilder.CreateIndex(
                name: "IX_asociacion_producto_id_producto",
                table: "asociacion_producto",
                column: "id_producto");

            migrationBuilder.AddForeignKey(
                name: "FK_asociacion_producto_detalle_insumo",
                table: "asociacion_producto",
                column: "id_detalle_insumo",
                principalTable: "detalle_insumo",
                principalColumn: "id_detalle");

            migrationBuilder.AddForeignKey(
                name: "FK_asociacion_producto_producto",
                table: "asociacion_producto",
                column: "id_producto",
                principalTable: "producto",
                principalColumn: "id_producto");

            migrationBuilder.AddForeignKey(
                name: "FK_detalle_insumo_insumo",
                table: "detalle_insumo",
                column: "id_insumo",
                principalTable: "insumo",
                principalColumn: "id_insumo");

            migrationBuilder.AddForeignKey(
                name: "FK_orden_venta_producto",
                table: "orden_venta",
                column: "id_producto",
                principalTable: "producto",
                principalColumn: "id_producto");

            migrationBuilder.AddForeignKey(
                name: "FK_orden_venta_venta",
                table: "orden_venta",
                column: "id_venta",
                principalTable: "venta",
                principalColumn: "id_venta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asociacion_producto_detalle_insumo",
                table: "asociacion_producto");

            migrationBuilder.DropForeignKey(
                name: "FK_asociacion_producto_producto",
                table: "asociacion_producto");

            migrationBuilder.DropForeignKey(
                name: "FK_detalle_insumo_insumo",
                table: "detalle_insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_orden_venta_producto",
                table: "orden_venta");

            migrationBuilder.DropForeignKey(
                name: "FK_orden_venta_venta",
                table: "orden_venta");

            migrationBuilder.DropIndex(
                name: "IX_orden_venta_id_producto",
                table: "orden_venta");

            migrationBuilder.DropIndex(
                name: "IX_orden_venta_id_venta",
                table: "orden_venta");

            migrationBuilder.DropIndex(
                name: "IX_asociacion_producto_id_detalle_insumo",
                table: "asociacion_producto");

            migrationBuilder.DropIndex(
                name: "IX_asociacion_producto_id_producto",
                table: "asociacion_producto");

            migrationBuilder.AddForeignKey(
                name: "FK__detalle_i__estad__412EB0B6",
                table: "detalle_insumo",
                column: "id_insumo",
                principalTable: "insumo",
                principalColumn: "id_insumo");
        }
    }
}
