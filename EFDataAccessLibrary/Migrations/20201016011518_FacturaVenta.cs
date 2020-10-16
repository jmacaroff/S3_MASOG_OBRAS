using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccessLibrary.Migrations
{
    public partial class FacturaVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturasVenta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProyectoID = table.Column<int>(nullable: false),
                    PuntoVenta = table.Column<string>(type: "nchar(5)", nullable: false),
                    TipoFactura = table.Column<string>(type: "char(1)", nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturasVenta_Proyectos_ProyectoID",
                        column: x => x.ProyectoID,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturasVentaItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaVentaId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<string>(type: "varchar(6)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Subtotal = table.Column<double>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasVentaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturasVentaItems_FacturasVenta_FacturaVentaId",
                        column: x => x.FacturaVentaId,
                        principalTable: "FacturasVenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturasVentaItems_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturasVenta_ProyectoID",
                table: "FacturasVenta",
                column: "ProyectoID");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasVentaItems_FacturaVentaId",
                table: "FacturasVentaItems",
                column: "FacturaVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasVentaItems_ProductoId",
                table: "FacturasVentaItems",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturasVentaItems");

            migrationBuilder.DropTable(
                name: "FacturasVenta");
        }
    }
}
