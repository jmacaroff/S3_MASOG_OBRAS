using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccessLibrary.Migrations
{
    public partial class Factura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Proveedores_ProveedorId",
                table: "Ordenes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenItems_Ordenes_OrdenId",
                table: "OrdenItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenItems_Productos_ProductoId",
                table: "OrdenItems");

            migrationBuilder.CreateTable(
                name: "FacturasCompra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorId = table.Column<int>(nullable: false),
                    PuntoVenta = table.Column<string>(type: "nchar(5)", nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    TipoFactura = table.Column<string>(type: "char(1)", nullable: false),
                    FechaFactura = table.Column<DateTime>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturasCompra_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturaCompraItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaCompraId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<string>(type: "varchar(6)", nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaCompraItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaCompraItems_FacturasCompra_FacturaCompraId",
                        column: x => x.FacturaCompraId,
                        principalTable: "FacturasCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturaCompraItems_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturaCompraItems_FacturaCompraId",
                table: "FacturaCompraItems",
                column: "FacturaCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaCompraItems_ProductoId",
                table: "FacturaCompraItems",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasCompra_ProveedorId",
                table: "FacturasCompra",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Proveedores_ProveedorId",
                table: "Ordenes",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenItems_Ordenes_OrdenId",
                table: "OrdenItems",
                column: "OrdenId",
                principalTable: "Ordenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenItems_Productos_ProductoId",
                table: "OrdenItems",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Proveedores_ProveedorId",
                table: "Ordenes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenItems_Ordenes_OrdenId",
                table: "OrdenItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenItems_Productos_ProductoId",
                table: "OrdenItems");

            migrationBuilder.DropTable(
                name: "FacturaCompraItems");

            migrationBuilder.DropTable(
                name: "FacturasCompra");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Proveedores_ProveedorId",
                table: "Ordenes",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenItems_Ordenes_OrdenId",
                table: "OrdenItems",
                column: "OrdenId",
                principalTable: "Ordenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenItems_Productos_ProductoId",
                table: "OrdenItems",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
