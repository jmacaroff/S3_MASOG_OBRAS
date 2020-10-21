using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccessLibrary.Migrations
{
    public partial class FacturaVentaRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturasVenta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: false),
                    PuntoVenta = table.Column<string>(type: "nchar(5)", nullable: false),
                    TipoFactura = table.Column<string>(type: "char(1)", nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false),
                    PendienteCobrar = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturasVenta_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturasVenta_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recibos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(nullable: false),
                    FechaEmision = table.Column<DateTime>(nullable: false),
                    ConceptoPagoId = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recibos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recibos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recibos_ConceptoPago_ConceptoPagoId",
                        column: x => x.ConceptoPagoId,
                        principalTable: "ConceptoPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturaVentaItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaVentaId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<string>(type: "varchar(6)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaVentaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaVentaItems_FacturasVenta_FacturaVentaId",
                        column: x => x.FacturaVentaId,
                        principalTable: "FacturasVenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaVentaItems_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReciboItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReciboId = table.Column<int>(nullable: false),
                    FacturaVentaId = table.Column<int>(nullable: false),
                    Importe = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReciboItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReciboItems_FacturasVenta_FacturaVentaId",
                        column: x => x.FacturaVentaId,
                        principalTable: "FacturasVenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReciboItems_Recibos_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "Recibos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturasVenta_ClienteId",
                table: "FacturasVenta",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasVenta_ProyectoId",
                table: "FacturasVenta",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaVentaItems_FacturaVentaId",
                table: "FacturaVentaItems",
                column: "FacturaVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaVentaItems_ProductoId",
                table: "FacturaVentaItems",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciboItems_FacturaVentaId",
                table: "ReciboItems",
                column: "FacturaVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciboItems_ReciboId",
                table: "ReciboItems",
                column: "ReciboId");

            migrationBuilder.CreateIndex(
                name: "IX_Recibos_ClienteId",
                table: "Recibos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Recibos_ConceptoPagoId",
                table: "Recibos",
                column: "ConceptoPagoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaVentaItems");

            migrationBuilder.DropTable(
                name: "ReciboItems");

            migrationBuilder.DropTable(
                name: "FacturasVenta");

            migrationBuilder.DropTable(
                name: "Recibos");
        }
    }
}
