﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccessLibrary.Migrations
{
    public partial class FacturaOrdenPago : Migration
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
                name: "ConceptoPago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacturasCompra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorId = table.Column<int>(nullable: false),
                    OrdenId = table.Column<int>(nullable: false),
                    PuntoVenta = table.Column<string>(type: "nchar(5)", nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    TipoFactura = table.Column<string>(type: "char(1)", nullable: false),
                    FechaFactura = table.Column<DateTime>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false),
                    PendientePago = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturasCompra_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturasCompra_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesPago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorId = table.Column<int>(nullable: false),
                    FechaEmision = table.Column<DateTime>(nullable: false),
                    ConceptoPagoId = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesPago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesPago_ConceptoPago_ConceptoPagoId",
                        column: x => x.ConceptoPagoId,
                        principalTable: "ConceptoPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesPago_Proveedores_ProveedorId",
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaCompraItems_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenPagoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenPagoId = table.Column<int>(nullable: false),
                    FacturaCompraId = table.Column<int>(nullable: false),
                    Importe = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenPagoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenPagoItems_FacturasCompra_FacturaCompraId",
                        column: x => x.FacturaCompraId,
                        principalTable: "FacturasCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenPagoItems_OrdenesPago_OrdenPagoId",
                        column: x => x.OrdenPagoId,
                        principalTable: "OrdenesPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_FacturasCompra_OrdenId",
                table: "FacturasCompra",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasCompra_ProveedorId",
                table: "FacturasCompra",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesPago_ConceptoPagoId",
                table: "OrdenesPago",
                column: "ConceptoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesPago_ProveedorId",
                table: "OrdenesPago",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPagoItems_FacturaCompraId",
                table: "OrdenPagoItems",
                column: "FacturaCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPagoItems_OrdenPagoId",
                table: "OrdenPagoItems",
                column: "OrdenPagoId");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenItems_Productos_ProductoId",
                table: "OrdenItems",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            string query = "";

            query = query + "INSERT [dbo].[ConceptoPago] ([Descripcion]) VALUES ('Efectivo'), ('Tarjeta de Crédito'), ('Tarjeta de Dédito'), ('Cheque')";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturasCompra] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[FacturasCompra] ([Id], [ProveedorId], [OrdenId], [PuntoVenta], [Numero], [TipoFactura], [FechaFactura], [FechaAlta], [Observacion], [Total], [PendientePago]) VALUES  ";
            query = query + "(8, 3, 12, 167, 50674, N'A', CAST(N'2020-09-20T00:00:00.0000000' AS DateTime2), CAST(N'2020-09-25T00:00:00.0000000' AS DateTime2), N'Concepto de Pago por rep. placa durlock', CAST(178709.00 AS Decimal(18, 2)), CAST(178709.00 AS Decimal(18, 2)) ), "; 
            query = query + "(11, 9, 14, 1356, 2343564, N'A', CAST(N'2020-10-02T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-04T00:00:00.0000000' AS DateTime2), N'Concepto de Pago por rep. tornillos', CAST(257500.00 AS Decimal(18, 2)), CAST(257500.00 AS Decimal(18, 2)) ),";
            query = query + "(13, 4, 18, 8, 10867, N'A', CAST(N'2020-10-05T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-05T00:00:00.0000000' AS DateTime2), N'Concepto de Pago por flete Ord. 14', CAST(2600.00 AS Decimal(18, 2)), CAST(2600.00 AS Decimal(18, 2)) )";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturasCompra] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturaCompraItems] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[FacturaCompraItems] ([Id], [FacturaCompraId], [ProductoId], [Cantidad], [Precio], [Observacion]) VALUES  ";
            query = query + "(5, 8, N'DUR001', 100, CAST(970.45 AS Decimal(18, 2)), NULL), ";
            query = query + "(6, 8, N'DUR002', 80, CAST(1020.80 AS Decimal(18, 2)), NULL), ";
            query = query + "(10, 11, N'TOR001', 5000, CAST(9.00 AS Decimal(18, 2)), NULL), ";
            query = query + "(11, 11, N'TOR002', 10000, CAST(11.80 AS Decimal(18, 2)), NULL), ";
            query = query + "(12, 11, N'TOR101', 10000, CAST(9.45 AS Decimal(18, 2)), NULL), ";
            query = query + "(14, 13, N'FLT001', 10000, CAST(9.45 AS Decimal(18, 2)), NULL) ";


            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturaCompraItems] OFF ";

            migrationBuilder.Sql(query);
            query = "";

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
                name: "OrdenPagoItems");

            migrationBuilder.DropTable(
                name: "FacturasCompra");

            migrationBuilder.DropTable(
                name: "OrdenesPago");

            migrationBuilder.DropTable(
                name: "ConceptoPago");

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
