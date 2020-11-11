using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccessLibrary.Migrations
{
    public partial class Stock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<string>(type: "varchar(6)", nullable: false),
                    DepositoId = table.Column<string>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_DepositoId",
                table: "Stock",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductoId",
                table: "Stock",
                column: "ProductoId");

            string query = "";

            query = query + "INSERT [dbo].[Depositos] ([Id], [Descripcion], [Direccion]) VALUES ";
            query = query + "(N'001', N'Depósito Centro', N'Santiago del Estero 1946'), ";
            query = query + "(N'002', N'Parque Industrial', N'Calle 2 entre 3 y 4'), ";
            query = query + "(N'999', N'Oficina Comercial', N'Leguizamon 1765') ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'GV0001', N'Informática', CAST(50000 AS Decimal(18, 2)), N'Todo lo relacionado con informática', 1)  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'LNV001', N'Lana de Vidrio Fieltro Liviano', CAST(789.20 AS Decimal(18, 2)), N'', 1)  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'LNV002', N'Lana de Vidrio Exterior', CAST(978.83 AS Decimal(18, 2)), N'', 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'DUR001', N'Placa Durlock Interior', CAST(1230.20 AS Decimal(18, 2)), N'No usar para techos!', 1)  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'DUR002', N'Placa Durlock Interior Calor', CAST(1305.83 AS Decimal(18, 2)), N'Especial cocina', 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'DUR003', N'Placa Durlock Interior Humedad', CAST(1356.23 AS Decimal(18, 2)), N'P/ baños', 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'DUR101', N'Placa Durlock Exterior', CAST(1698.37 AS Decimal(18, 2)), NULL, 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'DUR102', N'Placa Durlock Exterior p/ fuego', CAST(1876.20 AS Decimal(18, 2)), N'Especial parrilla', 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'DUR103', N'Placa Durlock Doble Exterior ', CAST(1750.00 AS Decimal(18, 2)), N'Doble protección', 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'DUR104', N'Placa Durlock Exterior Techo', CAST(1340.60 AS Decimal(18, 2)), N'No viene más', 0) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'FLT001', N'Fletes Nacionales', CAST(1706.78 AS Decimal(18, 2)), NULL, 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'PAS001', N'Pasaje doméstico', CAST(5000.00 AS Decimal(18, 2)), NULL, 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'PAS002', N'Pasaje internacional', CAST(8000.00 AS Decimal(18, 2)), NULL, 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'TOR001', N'Tornillo H1 Nº1', CAST(12.30 AS Decimal(18, 2)), N'Especial para steel', 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'TOR002', N'Tornillo H1 Nº2', CAST(15.50 AS Decimal(18, 2)), NULL, 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Productos] ([Id], [Descripcion], [Precio], [Observacion], [Activo]) VALUES  ";
            query = query + "(N'TOR101', N'Tornillo H2 Nº2', CAST(13.25 AS Decimal(18, 2)), NULL, 1) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Proveedores] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Proveedores] ([Id], [RazonSocial], [CUIT], [Direccion], [Telefono], [Correo], [Observacion]) VALUES  ";
            query = query + "(3, N'Durlock SA', 30707876234, N'Libertador 1937, CABA', 1144567281, N'info@durlock.com', N'Flete propio'), ";
            query = query + "(4, N'La Sevillanita', 30987256124, NULL, NULL, N'envios@lasevillanita.com.ar', NULL), ";
            query = query + "(5, N'Banco Macro', 30289376453, NULL, NULL, NULL, N'P/ cuenta cheques'), ";
            query = query + "(6, N'Banco Credicoop', 39038948298, NULL, NULL, NULL, NULL), ";
            query = query + "(7, N'Aerolíneas Argentinas', 27354618293, NULL, NULL, N'info@aerolineas.com.ar', NULL), ";
            query = query + "(8, N'Tel-Fix', 23923494739, NULL, NULL, NULL, N'Tornillos'), ";
            query = query + "(9, N'Isover', 30636243601, N'Congreso 2832, CABA', 541145647896, N'info @saintgobain.com', N'Lanas de vidrio'), ";
            query = query + "(10, N'Balder SRL', 24851582692, N'Indalecio Gomez 226', 3874522805, N'info @balder.com', N'Desarrollo sistema gestión') ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Proveedores] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Ordenes] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Ordenes] ([Id], [ProveedorId], [Fecha], [FechaEntrega], [Observacion]) VALUES   ";
            query = query + "(12, 3, CAST(N'2020-01-03T00:00:00.0000000' AS DateTime2), CAST(N'2020-01-17T00:00:00.0000000' AS DateTime2), N'Entrega demorada p/ cuarentena'),  ";
            query = query + "(14, 8, CAST(N'2020-01-23T00:00:00.0000000' AS DateTime2), CAST(N'2020-01-29T00:00:00.0000000' AS DateTime2), N'Entrega a tiempo'),  ";
            query = query + "(17, 7, CAST(N'2020-01-30T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-11T00:00:00.0000000' AS DateTime2), N'Pasaje Braga'),  ";
            query = query + "(18, 4, CAST(N'2020-02-05T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-15T00:00:00.0000000' AS DateTime2), N'Flete Tel-Fix Ord. 14'),  ";
            query = query + "(19, 8, CAST(N'2020-02-18T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-28T00:00:00.0000000' AS DateTime2), N'Entrega a tiempo'),  ";
            query = query + "(20, 4, CAST(N'2020-02-28T00:00:00.0000000' AS DateTime2), CAST(N'2020-03-12T00:00:00.0000000' AS DateTime2), N'Flete Tel-Fix Ord. 19'),  ";
            query = query + "(24, 4, CAST(N'2020-03-04T00:00:00.0000000' AS DateTime2), CAST(N'2020-03-12T00:00:00.0000000' AS DateTime2), N'Flete Durlock'),  ";
            query = query + "(25, 3, CAST(N'2020-03-26T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-11T00:00:00.0000000' AS DateTime2), N'Placas interiores varias'),  ";
            query = query + "(26, 8, CAST(N'2020-04-04T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-17T00:00:00.0000000' AS DateTime2), N'Tornillos H1'),  ";
            query = query + "(27, 4, CAST(N'2020-04-05T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-14T00:00:00.0000000' AS DateTime2), N'Flete Tel-Fix'),  ";
            query = query + "(28, 9, CAST(N'2020-05-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-19T00:00:00.0000000' AS DateTime2), N'Flete a cargo de Isover'),  ";
            query = query + "(29, 4, CAST(N'2020-05-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-05-15T00:00:00.0000000' AS DateTime2), N'Envío atrasado Durlock. Ord 25.'),  ";
            query = query + "(30, 7, CAST(N'2020-06-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-06-01T00:00:00.0000000' AS DateTime2), N'Pasaje Sao Paulo Macaroff'),  ";
            query = query + "(31, 8, CAST(N'2020-06-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-20T00:00:00.0000000' AS DateTime2), N'Tornillos H2'),  ";
            query = query + "(32, 4, CAST(N'2020-07-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-07-06T00:00:00.0000000' AS DateTime2), N'Envío atrasado Tel-Fix '),  ";
            query = query + "(33, 3, CAST(N'2020-07-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-19T00:00:00.0000000' AS DateTime2), N'Entrega a cargo de Durlock'),  ";
            query = query + "(34, 4, CAST(N'2020-08-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-11T00:00:00.0000000' AS DateTime2), N'Entregas a Bs As'),  ";
            query = query + "(36, 9, CAST(N'2020-09-12T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-19T00:00:00.0000000' AS DateTime2), N'Lanas de Vidrio Obra Saluzzi'),  ";
            query = query + "(37, 4, CAST(N'2020-09-13T00:00:00.0000000' AS DateTime2), CAST(N'2020-09-19T00:00:00.0000000' AS DateTime2), N'Envío Orden Saluzzi'),  ";
            query = query + "(38, 8, CAST(N'2020-10-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-21T00:00:00.0000000' AS DateTime2), N'Envío a cargo de La Sevillanita'),  ";
            query = query + "(39, 4, CAST(N'2020-10-12T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-21T00:00:00.0000000' AS DateTime2), N'Ord. 38 Tel-Fix Flete'),  ";
            query = query + "(40, 3, CAST(N'2020-11-06T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-12T00:00:00.0000000' AS DateTime2), N'Entrega incluída')  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Ordenes] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[OrdenItems] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[OrdenItems] ([Id], [OrdenId], [ProductoId], [Cantidad], [Precio], [Observacion]) VALUES   ";
            query = query + "(3, 12, N'DUR001', 100, CAST(970.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(4, 12, N'DUR002', 80, CAST(1020.80 AS Decimal(18, 2)), NULL),  ";
            query = query + "(6, 14, N'TOR001', 5000, CAST(9.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(7, 14, N'TOR002', 10000, CAST(11.80 AS Decimal(18, 2)), NULL),  ";
            query = query + "(9, 14, N'TOR101', 10000, CAST(9.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(10, 17, N'PAS001', 1, CAST(6890.58 AS Decimal(18, 2)), NULL),  ";
            query = query + "(12, 18, N'FLT001', 1, CAST(2600.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(13, 19, N'TOR002', 20000, CAST(9.34 AS Decimal(18, 2)), NULL),  ";
            query = query + "(15, 19, N'TOR001', 15000, CAST(8.92 AS Decimal(18, 2)), NULL),  ";
            query = query + "(16, 20, N'FLT001', 1, CAST(2650.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(20, 24, N'FLT001', 1, CAST(1986.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(21, 25, N'DUR001', 500, CAST(1230.20 AS Decimal(18, 2)), NULL),  ";
            query = query + "(22, 25, N'DUR002', 150, CAST(1305.83 AS Decimal(18, 2)), NULL),  ";
            query = query + "(23, 25, N'DUR003', 200, CAST(1356.23 AS Decimal(18, 2)), NULL),  ";
            query = query + "(24, 26, N'TOR001', 30000, CAST(12.30 AS Decimal(18, 2)), NULL),  ";
            query = query + "(25, 26, N'TOR002', 25000, CAST(15.50 AS Decimal(18, 2)), NULL),  ";
            query = query + "(26, 27, N'FLT001', 1, CAST(1700.78 AS Decimal(18, 2)), NULL),  ";
            query = query + "(27, 28, N'LNV001', 750, CAST(890.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(28, 28, N'LNV002', 980, CAST(960.30 AS Decimal(18, 2)), NULL),  ";
            query = query + "(29, 29, N'FLT001', 1, CAST(2090.12 AS Decimal(18, 2)), NULL),  ";
            query = query + "(30, 30, N'PAS002', 1, CAST(35123.40 AS Decimal(18, 2)), NULL),  ";
            query = query + "(31, 31, N'TOR101', 35000, CAST(13.25 AS Decimal(18, 2)), NULL),  ";
            query = query + "(32, 32, N'FLT001', 1, CAST(1706.78 AS Decimal(18, 2)), NULL),  ";
            query = query + "(33, 33, N'DUR101', 350, CAST(1698.37 AS Decimal(18, 2)), NULL),  ";
            query = query + "(34, 33, N'DUR102', 50, CAST(1876.20 AS Decimal(18, 2)), NULL),  ";
            query = query + "(35, 33, N'FLT001', 1, CAST(2509.02 AS Decimal(18, 2)), NULL),  ";
            query = query + "(36, 34, N'FLT001', 1, CAST(7892.01 AS Decimal(18, 2)), NULL),  ";
            query = query + "(39, 36, N'LNV001', 50, CAST(890.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(40, 36, N'LNV002', 60, CAST(960.30 AS Decimal(18, 2)), NULL),  ";
            query = query + "(41, 37, N'FLT001', 1, CAST(970.46 AS Decimal(18, 2)), NULL),  ";
            query = query + "(42, 38, N'TOR001', 18000, CAST(12.30 AS Decimal(18, 2)), NULL),  ";
            query = query + "(43, 38, N'TOR002', 20000, CAST(15.50 AS Decimal(18, 2)), NULL),  ";
            query = query + "(44, 39, N'FLT001', 1, CAST(1706.78 AS Decimal(18, 2)), NULL),  ";
            query = query + "(45, 40, N'DUR001', 300, CAST(1230.20 AS Decimal(18, 2)), NULL),  ";
            query = query + "(46, 40, N'DUR002', 50, CAST(1305.83 AS Decimal(18, 2)), NULL),  ";
            query = query + "(47, 40, N'FLT001', 1, CAST(1706.78 AS Decimal(18, 2)), NULL) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[OrdenItems] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Clientes] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Clientes] ([Id], [Nombre], [DNI], [Direccion], [Telefono], [Correo]) VALUES  ";
            query = query + "(1, N'Sebastián Braga', 40934582, N'Indalecio Gómez 226', 3874522805, N'sebabraga01@gmail.com'), ";
            query = query + "(3, N'Ramón Domínguez', 13456723, N'Caseros 283', 3876241896, NULL), ";
            query = query + "(4, N'Marcela Urribarri', 25671024, NULL, NULL, N'murribarri@gmail.com'), ";
            query = query + "(6, N'Campastro Eustacio', 23418761, NULL, NULL, NULL), ";
            query = query + "(7, N'Roman Riquelme', 29647120, N'Brandsen 805', NULL, N'elmasgrande@gmail.com'), ";
            query = query + "(8, N'La Paulina SRL', 30707545664, N'Reyes Católicos 456', 3872348764, N'info@lapaulina.com'), ";
            query = query + "(9, N'Gutierrez Hermanos', 30575872832, NULL, 3852548448, NULL),  ";
            query = query + "(10, N'Adolfo Alsina', 18564762, NULL, NULL, N'aalsina45@gmail.com') ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Clientes] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[ConceptoPago] ([Descripcion]) VALUES ('Transferencia'), ('Tarjeta de Crédito'), ('Tarjeta de Dédito'), ('Cheque'), ('Efectivo')";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturasCompra] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[FacturasCompra] ([Id], [ProveedorId], [OrdenId], [PuntoVenta], [Numero], [TipoFactura], [FechaFactura], [FechaAlta], [Observacion], [Total], [PendientePago]) VALUES   ";
            query = query + "(8, 3, 12, N'167  ', 50674, N'A', CAST(N'2020-01-20T00:00:00.0000000' AS DateTime2), CAST(N'2020-01-25T00:00:00.0000000' AS DateTime2), N'Concepto de Pago por rep. placa durlock', 178709, 0),  ";
            query = query + "(11, 8, 14, N'1356 ', 2343564, N'A', CAST(N'2020-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-04T00:00:00.0000000' AS DateTime2), N'Concepto de Pago por rep. tornillos', 257500, 0),  ";
            query = query + "(13, 4, 18, N'8    ', 10867, N'A', CAST(N'2020-02-20T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-25T00:00:00.0000000' AS DateTime2), N'Concepto de Pago por flete Ord. 14', 2600, 0),  ";
            query = query + "(15, 7, 17, N'345  ', 756753, N'A', CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), CAST(N'2020-02-05T00:00:00.0000000' AS DateTime2), NULL, 6890.58, 0),  ";
            query = query + "(16, 3, 25, N'167  ', 51200, N'A', CAST(N'2020-03-05T00:00:00.0000000' AS DateTime2), CAST(N'2020-03-11T00:00:00.0000000' AS DateTime2), NULL, 378720.5, 0),  ";
            query = query + "(17, 8, 19, N'1356 ', 2343579, N'A', CAST(N'2020-03-12T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-15T00:00:00.0000000' AS DateTime2), N'Tornillos H1', 320600, 0),  ";
            query = query + "(18, 4, 20, N'8    ', 11006, N'A', CAST(N'2020-04-08T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-11T00:00:00.0000000' AS DateTime2), N'Flete', 2650, 0),  ";
            query = query + "(19, 3, 33, N'167  ', 51345, N'A', CAST(N'2020-04-22T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-25T00:00:00.0000000' AS DateTime2), N'Durlock Exteriores', 690748.52, 0),  ";
            query = query + "(20, 8, 26, N'1356 ', 2343609, N'A', CAST(N'2020-05-12T00:00:00.0000000' AS DateTime2), CAST(N'2020-05-14T00:00:00.0000000' AS DateTime2), NULL, 756500, 0),  ";
            query = query + "(21, 4, 24, N'8    ', 11456, N'A', CAST(N'2020-05-21T00:00:00.0000000' AS DateTime2), CAST(N'2020-05-23T00:00:00.0000000' AS DateTime2), NULL, 1986, 0),  ";
            query = query + "(22, 4, 27, N'8    ', 2343656, N'A', CAST(N'2020-06-05T00:00:00.0000000' AS DateTime2), CAST(N'2020-06-11T00:00:00.0000000' AS DateTime2), NULL, 1700.78, 0),  ";
            query = query + "(23, 9, 28, N'12   ', 45646, N'A', CAST(N'2020-06-10T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-14T00:00:00.0000000' AS DateTime2), NULL, 1608931.5, 0),  ";
            query = query + "(24, 3, 40, N'167  ', 51404, N'A', CAST(N'2020-07-15T00:00:00.0000000' AS DateTime2), CAST(N'2020-07-18T00:00:00.0000000' AS DateTime2), NULL, 436058.28, 0),  ";
            query = query + "(25, 4, 29, N'8    ', 11567, N'A', CAST(N'2020-07-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-17T00:00:00.0000000' AS DateTime2), NULL, 2090.12, 0),  ";
            query = query + "(26, 4, 32, N'8    ', 11612, N'A', CAST(N'2020-07-22T00:00:00.0000000' AS DateTime2), CAST(N'2020-07-24T00:00:00.0000000' AS DateTime2), NULL, 1706.78, 0),  ";
            query = query + "(27, 8, 31, N'1356 ', 2343700, N'A', CAST(N'2020-08-13T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-17T00:00:00.0000000' AS DateTime2), NULL, 463750, 0),  ";
            query = query + "(28, 7, 30, N'345  ', 786753, N'A', CAST(N'2020-08-15T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-22T00:00:00.0000000' AS DateTime2), NULL, 35123.4, 0),  ";
            query = query + "(29, 4, 34, N'8    ', 12908, N'A', CAST(N'2020-09-11T00:00:00.0000000' AS DateTime2), CAST(N'2020-09-14T00:00:00.0000000' AS DateTime2), NULL, 7892.01, 0),  ";
            query = query + "(30, 4, 37, N'8    ', 12268, N'A', CAST(N'2020-08-12T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-14T00:00:00.0000000' AS DateTime2), NULL, 970.46, 0),  ";
            query = query + "(32, 8, 38, N'1356 ', 2343823, N'A', CAST(N'2020-10-08T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-12T00:00:00.0000000' AS DateTime2), NULL, 531400, 0),  ";
            query = query + "(33, 4, 39, N'8    ', 12457, N'A', CAST(N'2020-10-22T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-25T00:00:00.0000000' AS DateTime2), NULL, 1706.78, 0),  ";
            query = query + "(34, 9, 36, N'12   ', 47789, N'A', CAST(N'2020-11-06T00:00:00.0000000' AS DateTime2), CAST(N'2020-11-09T00:00:00.0000000' AS DateTime2), NULL, 102140.5, 0)   ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturasCompra] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturaCompraItems] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[FacturaCompraItems] ([Id], [FacturaCompraId], [ProductoId], [Cantidad], [Precio], [Observacion]) VALUES  ";
            query = query + "(5, 8, N'DUR001', 100, CAST(970.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(6, 8, N'DUR002', 80, CAST(1020.80 AS Decimal(18, 2)), NULL),  ";
            query = query + "(10, 11, N'TOR001', 5000, CAST(9.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(11, 11, N'TOR002', 10000, CAST(11.80 AS Decimal(18, 2)), NULL),  ";
            query = query + "(12, 11, N'TOR101', 10000, CAST(9.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(14, 13, N'FLT001', 1, CAST(2600.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(16, 15, N'PAS001', 1, CAST(6890.58 AS Decimal(18, 2)), NULL),  ";
            query = query + "(17, 16, N'DUR001', 500, CAST(1230.20 AS Decimal(18, 2)), NULL),  ";
            query = query + "(18, 16, N'DUR002', 150, CAST(1305.83 AS Decimal(18, 2)), NULL),  ";
            query = query + "(19, 16, N'DUR003', 200, CAST(1356.23 AS Decimal(18, 2)), NULL),  ";
            query = query + "(20, 17, N'TOR002', 20000, CAST(9.34 AS Decimal(18, 2)), NULL),  ";
            query = query + "(21, 17, N'TOR001', 15000, CAST(8.92 AS Decimal(18, 2)), NULL),  ";
            query = query + "(22, 18, N'FLT001', 1, CAST(2650.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(23, 19, N'DUR101', 350, CAST(1698.37 AS Decimal(18, 2)), NULL),  ";
            query = query + "(24, 19, N'DUR102', 50, CAST(1876.20 AS Decimal(18, 2)), NULL),  ";
            query = query + "(25, 19, N'FLT001', 1, CAST(2509.02 AS Decimal(18, 2)), NULL),  ";
            query = query + "(26, 20, N'TOR001', 30000, CAST(12.30 AS Decimal(18, 2)), NULL),  ";
            query = query + "(27, 20, N'TOR002', 25000, CAST(15.50 AS Decimal(18, 2)), NULL),  ";
            query = query + "(28, 21, N'FLT001', 1, CAST(1986.00 AS Decimal(18, 2)), NULL),  ";
            query = query + "(29, 22, N'FLT001', 1, CAST(1700.78 AS Decimal(18, 2)), NULL),  ";
            query = query + "(30, 23, N'LNV001', 750, CAST(890.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(31, 23, N'LNV002', 980, CAST(960.30 AS Decimal(18, 2)), NULL),  ";
            query = query + "(32, 24, N'DUR001', 300, CAST(1230.20 AS Decimal(18, 2)), NULL),  ";
            query = query + "(33, 24, N'DUR002', 50, CAST(1305.83 AS Decimal(18, 2)), NULL),  ";
            query = query + "(34, 24, N'FLT001', 1, CAST(1706.78 AS Decimal(18, 2)), NULL),  ";
            query = query + "(35, 25, N'FLT001', 1, CAST(2090.12 AS Decimal(18, 2)), NULL),  ";
            query = query + "(36, 26, N'FLT001', 1, CAST(1706.78 AS Decimal(18, 2)), NULL),  ";
            query = query + "(37, 27, N'TOR101', 35000, CAST(13.25 AS Decimal(18, 2)), NULL),  ";
            query = query + "(38, 28, N'PAS002', 1, CAST(35123.40 AS Decimal(18, 2)), NULL),  ";
            query = query + "(39, 29, N'FLT001', 1, CAST(7892.01 AS Decimal(18, 2)), NULL),  ";
            query = query + "(40, 30, N'FLT001', 1, CAST(970.46 AS Decimal(18, 2)), NULL),  ";
            query = query + "(43, 32, N'TOR001', 18000, CAST(12.30 AS Decimal(18, 2)), NULL),  ";
            query = query + "(44, 32, N'TOR002', 20000, CAST(15.50 AS Decimal(18, 2)), NULL),  ";
            query = query + "(45, 33, N'FLT001', 1, CAST(1706.78 AS Decimal(18, 2)), NULL),  ";
            query = query + "(46, 34, N'LNV001', 50, CAST(890.45 AS Decimal(18, 2)), NULL),  ";
            query = query + "(47, 34, N'LNV002', 60, CAST(960.30 AS Decimal(18, 2)), NULL) ";


            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[FacturaCompraItems] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Proyectos] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Proyectos] ([Id], [ClienteId], [Nombre], [Direccion], [Observacion]) VALUES  ";
            query = query + "(1, 1, N'Vivienda Braga', N'Av. de Los Incas 3232', NULL), ";
            query = query + "(3, 4, N'Casa El Tipal Urribarri', N'Las Tipas casa 78', N'Fecha finalización estipulada 30/04/2021'), ";
            query = query + "(4, 8, N'Ampliación local La Paulina', N'Reyes Católicos 456', NULL), ";
            query = query + "(5, 9, N'Sucursal Carrefour', N'Hipólito Yrigoyen 5230', NULL) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Proyectos] OFF  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[TipoMovimiento] ([EsEgreso], [Descripcion]) " +
                "VALUES (0, 'Ingreso por Ajuste'), (0, 'Remito de Proveedor'), (0, 'Devolución de Obra')," +
                "(1, 'Egreso por Ajuste'), (1, 'Remito a Obra'), (1, 'Material Defectuoso')";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "CREATE TRIGGER InsertStock ";
            query = query + "ON MovStockItems ";
            query = query + "FOR INSERT ";
            query = query + "AS ";
            query = query + "declare @ProductoId varchar(6) ";
            query = query + "declare @DepositoId nchar(3) ";
            query = query + "declare @Cant int ";
            query = query + "declare @MovStockId int ";
            query = query + "declare @TMovId int ";
            query = query + "declare @Signo int ";
            query = query + "set @MovStockId = (SELECT TOP 1 MovStockId from inserted) ";
            query = query + "set @DepositoId = (SELECT DepositoId from MovsStock ";
            query = query + "                   where Id = @MovStockId) ";
            query = query + "set @TMovId = (SELECT TipoMovimientoId from MovsStock where Id = @MovStockId) ";
            query = query + "set @Signo = CASE ";
            query = query + "             WHEN((SELECT EsEgreso from TipoMovimiento where Id = @TMovId) = 1) THEN - 1 ";
            query = query + "             ELSE 1 END ";
            query = query + "declare cur CURSOR LOCAL for ";
            query = query + "    select ProductoId, Cantidad from inserted ";
            query = query + "open cur ";
            query = query + "fetch next from cur into @ProductoId, @Cant ";
            query = query + "while @@FETCH_STATUS = 0 ";
            query = query + "BEGIN ";
            query = query + "    IF(SELECT Cantidad from Stock Where ProductoId = @ProductoId and DepositoId = @DepositoId) is null ";
            query = query + "        BEGIN ";
            query = query + "        BEGIN TRAN ";
            query = query + "        INSERT INTO Stock(ProductoId, DepositoId, Cantidad) ";
            query = query + "        VALUES(@ProductoId, @DepositoId, @Cant * @Signo) ";
            query = query + "        COMMIT TRAN ";
            query = query + "        END ";
            query = query + "    ELSE ";
            query = query + "        BEGIN ";
            query = query + "        BEGIN TRAN ";
            query = query + "        UPDATE STOCK ";
            query = query + "        SET Cantidad = Cantidad + @Cant  * @Signo ";
            query = query + "        WHERE DepositoId = @DepositoId and ProductoId = @ProductoId ";
            query = query + "        COMMIT TRAN ";
            query = query + "        END ";
            query = query + "    fetch next from cur into @ProductoId, @Cant ";
            query = query + "END ";
            query = query + "close cur ";
            query = query + "deallocate cur ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT[dbo].[OrdenesPago] ON ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[OrdenesPago] ([Id], [ProveedorId], [FechaEmision], [ConceptoPagoId], [Observacion], [Total]) VALUES   ";
            query = query + "(1, 4, CAST(N'2020-02-15T00:00:00.0000000' AS DateTime2), 1, NULL, 2600),   ";
            query = query + "(2, 3, CAST(N'2020-02-22T00:00:00.0000000' AS DateTime2), 1, NULL, 178709),   ";
            query = query + "(3, 3, CAST(N'2020-03-11T00:00:00.0000000' AS DateTime2), 4, NULL, 378720.5),   ";
            query = query + "(4, 8, CAST(N'2020-02-13T00:00:00.0000000' AS DateTime2), 1, NULL, 257500),   ";
            query = query + "(5, 8, CAST(N'2020-03-12T00:00:00.0000000' AS DateTime2), 4, NULL, 320600),   ";
            query = query + "(6, 4, CAST(N'2020-04-15T00:00:00.0000000' AS DateTime2), 3, NULL, 2650),   ";
            query = query + "(7, 7, CAST(N'2020-03-11T00:00:00.0000000' AS DateTime2), 2, NULL, 6890.58),   ";
            query = query + "(8, 3, CAST(N'2020-04-22T00:00:00.0000000' AS DateTime2), 1, NULL, 690748.52),   ";
            query = query + "(9, 8, CAST(N'2020-05-13T00:00:00.0000000' AS DateTime2), 4, NULL, 756500),   ";
            query = query + "(10, 4, CAST(N'2020-05-31T00:00:00.0000000' AS DateTime2), 3, NULL, 1986),   ";
            query = query + "(11, 4, CAST(N'2020-06-15T00:00:00.0000000' AS DateTime2), 3, NULL, 1700.78),   ";
            query = query + "(12, 9, CAST(N'2020-06-28T00:00:00.0000000' AS DateTime2), 1, NULL, 1608931.5),   ";
            query = query + "(13, 4, CAST(N'2020-07-22T00:00:00.0000000' AS DateTime2), 3, NULL, 3796.89),   ";
            query = query + "(14, 7, CAST(N'2020-08-16T00:00:00.0000000' AS DateTime2), 2, NULL, 35123.4),   ";
            query = query + "(15, 4, CAST(N'2020-08-26T00:00:00.0000000' AS DateTime2), 3, NULL, 970.46),   ";
            query = query + "(16, 8, CAST(N'2020-08-30T00:00:00.0000000' AS DateTime2), 4, NULL, 463750),   ";
            query = query + "(17, 3, CAST(N'2020-09-04T00:00:00.0000000' AS DateTime2), 1, NULL, 436058.28),   ";
            query = query + "(19, 8, CAST(N'2020-10-12T00:00:00.0000000' AS DateTime2), 1, NULL, 531400),   ";
            query = query + "(20, 4, CAST(N'2020-10-22T00:00:00.0000000' AS DateTime2), 4, NULL, 9598.79),   ";
            query = query + "(21, 9, CAST(N'2020-11-09T00:00:00.0000000' AS DateTime2), 4, NULL, 102140.5)";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT[dbo].[OrdenesPago] OFF ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[OrdenPagoItems] ON ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT [dbo].[OrdenPagoItems] ([Id], [OrdenPagoId], [FacturaCompraId], [Importe]) VALUES   ";
            query = query + "(1, 1, 13, 2600),   ";
            query = query + "(2, 2, 8, 178709),   ";
            query = query + "(3, 3, 16, 378720.5),   ";
            query = query + "(4, 4, 11, 257500),   ";
            query = query + "(5, 5, 17, 320600),   ";
            query = query + "(6, 6, 18, 2650),   ";
            query = query + "(7, 7, 15, 6890.58),   ";
            query = query + "(8, 8, 19, 690748.52),   ";
            query = query + "(9, 9, 20, 756500),   ";
            query = query + "(10, 10, 21, 1986),   ";
            query = query + "(11, 11, 22, 1700.78),   ";
            query = query + "(12, 12, 23, 1608931.5),   ";
            query = query + "(13, 13, 25, 2090.12),   ";
            query = query + "(14, 13, 26, 1706.78),   ";
            query = query + "(15, 14, 28, 35123.4),   ";
            query = query + "(16, 15, 30, 970.46),   ";
            query = query + "(17, 16, 27, 463750),   ";
            query = query + "(18, 17, 24, 436058.28),   ";
            query = query + "(20, 19, 32, 531400),   ";
            query = query + "(21, 20, 29, 7892.01),   ";
            query = query + "(22, 20, 33, 1706.78),   ";
            query = query + "(23, 21, 34, 102140.5)";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[OrdenPagoItems] OFF ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[FacturasVenta] ON ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT[dbo].[FacturasVenta]([Id], [ClienteId], [ProyectoId], [PuntoVenta], [TipoFactura], [Numero], [Fecha], [Observacion], [Total], [PendienteCobrar]) VALUES  ";
            query = query + "(5, 1, 1, N'5    ', N'B', 456, CAST(N'2020-02-04T00:00:00.0000000' AS DateTime2), NULL, 218406.6, 0),  ";
            query = query + "(6, 4, 3, N'5    ', N'B', 457, CAST(N'2020-02-14T00:00:00.0000000' AS DateTime2), NULL, 333014.1, 0),  ";
            query = query + "(8, 1, 1, N'5    ', N'B', 455, CAST(N'2020-02-04T00:00:00.0000000' AS DateTime2), N'Tornillos Obra Braga', 21840, 0),  ";
            query = query + "(9, 4, 3, N'5    ', N'B', 458, CAST(N'2020-02-14T00:00:00.0000000' AS DateTime2), N'Tornillos Obra Urribarri', 27300, 0),  ";
            query = query + "(11, 8, 4, N'5    ', N'A', 158, CAST(N'2020-03-11T00:00:00.0000000' AS DateTime2), N'Factura inicial LP SRL', 1557076.6, 0),  ";
            query = query + "(12, 9, 5, N'5    ', N'A', 159, CAST(N'2020-04-15T00:00:00.0000000' AS DateTime2), N'Factura inicial Carrefour', 3658004, 0),  ";
            query = query + "(13, 1, 1, N'5    ', N'B', 459, CAST(N'2020-05-13T00:00:00.0000000' AS DateTime2), N'Factura 50% (20%)', 58895.76, 0),  ";
            query = query + "(14, 4, 3, N'5    ', N'B', 460, CAST(N'2020-05-28T00:00:00.0000000' AS DateTime2), N'Pago 60%', 114444.83, 0),  ";
            query = query + "(15, 8, 4, N'7    ', N'A', 1, CAST(N'2020-06-03T00:00:00.0000000' AS DateTime2), NULL, 800856, 0),  ";
            query = query + "(16, 9, 5, N'7    ', N'A', 2, CAST(N'2020-06-15T00:00:00.0000000' AS DateTime2), N'Factura 75%', 1512060, 0),  ";
            query = query + "(17, 1, 1, N'5    ', N'B', 461, CAST(N'2020-07-11T00:00:00.0000000' AS DateTime2), N'Factura 75%', 535724, 0),  ";
            query = query + "(18, 4, 3, N'5    ', N'B', 462, CAST(N'2020-08-11T00:00:00.0000000' AS DateTime2), N'Factura 75%', 823037, 0),  ";
            query = query + "(19, 8, 4, N'7    ', N'A', 3, CAST(N'2020-08-26T00:00:00.0000000' AS DateTime2), N'Factura 80%', 259059.6, 0),  ";
            query = query + "(20, 1, 1, N'5    ', N'B', 463, CAST(N'2020-09-22T00:00:00.0000000' AS DateTime2), NULL, 42282.3, 0),  ";
            query = query + "(21, 4, 3, N'5    ', N'B', 463, CAST(N'2020-10-08T00:00:00.0000000' AS DateTime2), N'Pago 100%', 87641.6, 0),  ";
            query = query + "(22, 8, 4, N'7    ', N'A', 4, CAST(N'2020-10-15T00:00:00.0000000' AS DateTime2), N'Pago final.', 418320, 0),  ";
            query = query + "(23, 9, 5, N'7    ', N'A', 5, CAST(N'2020-11-06T00:00:00.0000000' AS DateTime2), N'Reiniciación obra Carrefour ', 706312, 0)  ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[FacturasVenta] OFF ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[FacturaVentaItems] ON ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT [dbo].[FacturaVentaItems] ([Id], [FacturaVentaId], [ProductoId], [Precio], [Cantidad], [Observacion]) VALUES   ";
            query = query + "(7, 5, N'DUR101', CAST(3109.26 AS Decimal(18, 2)), 50, NULL),  ";
            query = query + "(8, 5, N'DUR001', CAST(2098.12 AS Decimal(18, 2)), 30, NULL),  ";
            query = query + "(9, 6, N'DUR001', CAST(2101.56 AS Decimal(18, 2)), 50, NULL),  ";
            query = query + "(10, 6, N'DUR101', CAST(3256.23 AS Decimal(18, 2)), 70, NULL),  ";
            query = query + "(13, 8, N'TOR001', CAST(18.20 AS Decimal(18, 2)), 1200, NULL),  ";
            query = query + "(14, 9, N'TOR001', CAST(18.20 AS Decimal(18, 2)), 1500, NULL),  ";
            query = query + "(17, 11, N'DUR001', CAST(1630.20 AS Decimal(18, 2)), 300, NULL),  ";
            query = query + "(18, 11, N'DUR101', CAST(2351.23 AS Decimal(18, 2)), 420, NULL),  ";
            query = query + "(19, 11, N'TOR001', CAST(16.10 AS Decimal(18, 2)), 5000, NULL),  ";
            query = query + "(20, 12, N'DUR001', CAST(1830.20 AS Decimal(18, 2)), 600, NULL),  ";
            query = query + "(21, 12, N'DUR101', CAST(3005.23 AS Decimal(18, 2)), 800, NULL),  ";
            query = query + "(22, 12, N'TOR001', CAST(17.30 AS Decimal(18, 2)), 9000, NULL),  ";
            query = query + "(23, 13, N'TOR101', CAST(21.50 AS Decimal(18, 2)), 1350, NULL),  ";
            query = query + "(24, 13, N'DUR003', CAST(2489.23 AS Decimal(18, 2)), 12, NULL),  ";
            query = query + "(25, 14, N'DUR002', CAST(2515.30 AS Decimal(18, 2)), 10, NULL),  ";
            query = query + "(26, 14, N'DUR003', CAST(2489.21 AS Decimal(18, 2)), 23, NULL),  ";
            query = query + "(27, 14, N'TOR101', CAST(21.36 AS Decimal(18, 2)), 1500, NULL),  ";
            query = query + "(28, 15, N'DUR104', CAST(2115.36 AS Decimal(18, 2)), 350, NULL),  ";
            query = query + "(29, 15, N'TOR101', CAST(18.90 AS Decimal(18, 2)), 3200, NULL),  ";
            query = query + "(30, 16, N'DUR104', CAST(2340.60 AS Decimal(18, 2)), 600, NULL),  ";
            query = query + "(31, 16, N'TOR101', CAST(17.95 AS Decimal(18, 2)), 6000, NULL),  ";
            query = query + "(32, 17, N'DUR104', CAST(2356.12 AS Decimal(18, 2)), 200, NULL),  ";
            query = query + "(33, 17, N'TOR101', CAST(21.50 AS Decimal(18, 2)), 3000, NULL),  ";
            query = query + "(34, 18, N'DUR104', CAST(2135.02 AS Decimal(18, 2)), 350, NULL),  ";
            query = query + "(35, 18, N'TOR101', CAST(21.05 AS Decimal(18, 2)), 3600, NULL),  ";
            query = query + "(36, 19, N'DUR003', CAST(1963.23 AS Decimal(18, 2)), 120, NULL),  ";
            query = query + "(37, 19, N'TOR101', CAST(19.56 AS Decimal(18, 2)), 1200, NULL),  ";
            query = query + "(38, 20, N'DUR102', CAST(3580.23 AS Decimal(18, 2)), 10, NULL),  ";
            query = query + "(39, 20, N'TOR001', CAST(21.60 AS Decimal(18, 2)), 300, NULL),  ";
            query = query + "(40, 21, N'DUR102', CAST(3523.60 AS Decimal(18, 2)), 15, NULL),  ";
            query = query + "(41, 21, N'TOR101', CAST(21.20 AS Decimal(18, 2)), 320, NULL),  ";
            query = query + "(42, 21, N'DUR003', CAST(2800.36 AS Decimal(18, 2)), 10, NULL),  ";
            query = query + "(43, 22, N'LNV001', CAST(1321.20 AS Decimal(18, 2)), 300, NULL),  ";
            query = query + "(44, 22, N'TOR101', CAST(18.30 AS Decimal(18, 2)), 1200, NULL),  ";
            query = query + "(45, 23, N'DUR103', CAST(3102.56 AS Decimal(18, 2)), 200, NULL),  ";
            query = query + "(46, 23, N'TOR002', CAST(28.60 AS Decimal(18, 2)), 3000, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[FacturaVentaItems] OFF ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[Recibos] ON ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT [dbo].[Recibos] ([Id], [ClienteId], [FechaEmision], [ConceptoPagoId], [Observacion], [Total]) VALUES   ";
            query = query + "(8, 1, CAST(N'2020-02-10T00:00:00.0000000' AS DateTime2), 4, NULL, 240246.6),  ";
            query = query + "(9, 4, CAST(N'2020-02-14T00:00:00.0000000' AS DateTime2), 1, N'Primer pago', 360314.1),  ";
            query = query + "(10, 8, CAST(N'2020-03-26T00:00:00.0000000' AS DateTime2), 4, NULL, 1557076.6),  ";
            query = query + "(11, 9, CAST(N'2020-04-30T00:00:00.0000000' AS DateTime2), 4, N'Pago 35%', 3658004),  ";
            query = query + "(12, 1, CAST(N'2020-05-15T00:00:00.0000000' AS DateTime2), 4, N'Pago 50%', 58895.76),  ";
            query = query + "(13, 4, CAST(N'2020-05-29T00:00:00.0000000' AS DateTime2), 1, N'Pago 60%', 114444.83),  ";
            query = query + "(14, 8, CAST(N'2020-06-18T00:00:00.0000000' AS DateTime2), 4, N'Pago 50%', 800856),  ";
            query = query + "(15, 9, CAST(N'2020-06-30T00:00:00.0000000' AS DateTime2), 4, N'Pago 75%', 1512060),  ";
            query = query + "(17, 1, CAST(N'2020-07-27T00:00:00.0000000' AS DateTime2), 4, NULL, 535724),  ";
            query = query + "(18, 4, CAST(N'2020-08-26T00:00:00.0000000' AS DateTime2), 4, N'Pago 75%', 823037),  ";
            query = query + "(19, 8, CAST(N'2020-09-10T00:00:00.0000000' AS DateTime2), 4, NULL, 259059.6),  ";
            query = query + "(20, 1, CAST(N'2020-10-11T00:00:00.0000000' AS DateTime2), 4, N'Pago final', 42282.3),  ";
            query = query + "(21, 4, CAST(N'2020-10-23T00:00:00.0000000' AS DateTime2), 1, N'Pago final.', 87641.6),  ";
            query = query + "(22, 8, CAST(N'2020-10-30T00:00:00.0000000' AS DateTime2), 1, N'Pago final.', 418320),  ";
            query = query + "(23, 9, CAST(N'2020-11-06T00:00:00.0000000' AS DateTime2), 4, N'Reiniciación obra Carrefour.', 706312) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[Recibos] OFF ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[ReciboItems] ON ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT [dbo].[ReciboItems] ([Id], [ReciboId], [FacturaVentaId], [Importe]) VALUES  ";
            query = query + "(9, 8, 5, 218406.6),  ";
            query = query + "(10, 8, 8, 21840),  ";
            query = query + "(11, 9, 6, 333014.1),  ";
            query = query + "(12, 9, 9, 27300),  ";
            query = query + "(13, 10, 11, 1557076.6),  ";
            query = query + "(14, 11, 12, 3658004),  ";
            query = query + "(15, 12, 13, 58895.76),  ";
            query = query + "(16, 13, 14, 114444.83),  ";
            query = query + "(17, 14, 15, 800856),  ";
            query = query + "(18, 15, 16, 1512060),  ";
            query = query + "(20, 17, 17, 535724),  ";
            query = query + "(21, 18, 18, 823037),  ";
            query = query + "(22, 19, 19, 259059.6),  ";
            query = query + "(23, 20, 20, 42282.3),  ";
            query = query + "(24, 21, 21, 87641.6),  ";
            query = query + "(25, 22, 22, 418320),  ";
            query = query + "(26, 23, 23, 706312) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[ReciboItems] OFF ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[MovsStock] ON ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovsStock]([Id], [TipoMovimientoId], [DepositoId], [ProyectoId], [ProveedorId], [Fecha], [Observacion]) VALUES(5, 2, N'002', NULL, 3, CAST(N'2020-10-23T21:24:10.8511128' AS DateTime2), NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovsStock] ([Id], [TipoMovimientoId], [DepositoId], [ProyectoId], [ProveedorId], [Fecha], [Observacion]) VALUES(6, 2, N'001', NULL, 8, CAST(N'2020-10-23T21:25:05.2544693' AS DateTime2), NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovsStock] ([Id], [TipoMovimientoId], [DepositoId], [ProyectoId], [ProveedorId], [Fecha], [Observacion]) VALUES(7, 5, N'002', 1, NULL, CAST(N'2020-10-23T21:26:02.3147253' AS DateTime2), NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[MovsStock] OFF ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "SET IDENTITY_INSERT[dbo].[MovStockItems] ON ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems]([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(16, 5, N'DUR001', 1500, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(17, 5, N'DUR002', 200, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(18, 5, N'DUR003', 500, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(19, 5, N'DUR101', 2000, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(20, 5, N'DUR102', 30, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(21, 5, N'DUR103', 1200, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(22, 6, N'TOR001', 20000, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
            
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(23, 6, N'TOR002', 15000, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
          
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(24, 6, N'TOR101', 17000, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
          
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(25, 7, N'DUR001', 70, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
     
            query = query + "INSERT[dbo].[MovStockItems] ([Id], [MovStockId], [ProductoId], [Cantidad], [Observacion]) VALUES(26, 7, N'DUR002', 5, NULL) ";

            migrationBuilder.Sql(query);
            query = "";
        
            query = query + "SET IDENTITY_INSERT[dbo].[MovStockItems] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "create view [dbo].[RecibosDet]  ";
            query = query + "as ";
            query = query + "select R.FechaEmision as FechaRecibo, R.ClienteId as ClienteId, C.Nombre as ClienteNombre, F.Numero as FacturaVentaNumero,  ";
            query = query + "FI.ProductoId as ProductoId, P.Descripcion as ProductoDescripcion, SUM(FI.Precio*Cantidad) as Total ";
            query = query + "from Recibos R, ReciboItems RI, FacturasVenta F, FacturaVentaItems FI, Clientes C, Productos P ";
            query = query + "where RI.ReciboId = R.Id ";
            query = query + "and RI.FacturaVentaId = F.Id and FI.FacturaVentaId = F.Id and R.ClienteId = C.Id and FI.ProductoId = P.Id ";
            query = query + "GROUP BY R.FechaEmision, R.ClienteId, C.Nombre, F.Numero, FI.ProductoId, P.Descripcion ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "create view [dbo].[OrdenesPagoDet]  ";
            query = query + "as ";
            query = query + "select OP.FechaEmision as FechaOrdenPago, OP.ProveedorId as ProveedorId, PR.RazonSocial as ProveedorNombre, F.Numero as FacturaCompraNumero,  ";
            query = query + "FI.ProductoId as ProductoId, P.Descripcion as ProductoDescripcion, SUM(FI.Precio*Cantidad) as Total ";
            query = query + "from OrdenesPago OP, OrdenPagoItems OPI, FacturasCompra F, FacturaCompraItems FI, Proveedores PR, Productos P ";
            query = query + "where OPI.OrdenPagoId = OP.Id ";
            query = query + "and OPI.FacturaCompraId = F.Id and FI.FacturaCompraId = F.Id and OP.ProveedorId = PR.Id and FI.ProductoId = P.Id ";
            query = query + "GROUP BY OP.FechaEmision, OP.ProveedorId, PR.RazonSocial, F.Numero, FI.ProductoId, P.Descripcion ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + " CREATE TABLE dbo.Dates( ";
            query = query + " DateID int NOT NULL IDENTITY(1, 1), ";
            query = query + "  [Date] date NOT NULL, ";
            query = query + "  [Year] int NOT NULL, ";
            query = query + "  [Month] int NOT NULL, ";
            query = query + "  [Day] int NOT NULL, ";
            query = query + "  [QuarterNumber] int NOT NULL, ";
            query = query + "  CONSTRAINT PK_Dates PRIMARY KEY CLUSTERED(DateID) ";
            query = query + " ) ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + " DECLARE @StartDate date ";
            query = query + " DECLARE @EndDate date ";
            query = query + " SET @StartDate = { d'2018-01-01' } ";
            query = query + " SET @EndDate = { d'2021-12-31' } ";
            query = query + " DECLARE @LoopDate date ";
            query = query + " SET @LoopDate = @StartDate ";
            query = query + " WHILE @LoopDate <= @EndDate ";
            query = query + " BEGIN ";
            query = query + "  INSERT INTO Dates VALUES( ";
            query = query + "   @LoopDate, ";
            query = query + "   Year(@LoopDate), ";
            query = query + "   Month(@LoopDate), ";
            query = query + "   Day(@LoopDate), ";
            query = query + "   CASE WHEN Month(@LoopDate) IN(1, 2, 3) THEN 1 ";
            query = query + "    WHEN Month(@LoopDate) IN(4, 5, 6) THEN 2 ";
            query = query + "    WHEN Month(@LoopDate) IN(7, 8, 9) THEN 3 ";
            query = query + "    WHEN Month(@LoopDate) IN(10, 11, 12) THEN 4 ";
            query = query + "   END ";
            query = query + "  ) ";
            query = query + "  SET @LoopDate = DateAdd(M, 1, @LoopDate) ";
            query = query + " END ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "create view [dbo].[Comparativo]  ";
            query = query + "as ";
            query = query + " SELECT [Year] Año, [Month] Mes,  ";
            query = query + " IsNull((SELECT SUM(Total) FROM Recibos ";
            query = query + " WHERE MONTH(FechaEmision) = [Month] ";
            query = query + " AND YEAR(FechaEmision) = [Year]), 0) Ingresos,  ";
            query = query + " IsNull((SELECT SUM(Total) FROM OrdenesPago ";
            query = query + " WHERE MONTH(FechaEmision) = [Month] ";
            query = query + " AND YEAR(FechaEmision) = [Year]), 0) Egresos ";
            query = query + " FROM Dates ";
            query = query + " GROUP BY [Year], [Month] ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + " create view [dbo].[RankingProductos]    ";
            query = query + " as  ";
            query = query + " select FI.ProductoId ProductoId, P.Descripcion Descripcion, SUM(FI.Cantidad) Ventas  ";
            query = query + " from FacturaVentaItems FI, Productos P  ";
            query = query + " where FI.ProductoId = P.Id  ";
            query = query + " group by FI.ProductoId, P.Descripcion  ";

            migrationBuilder.Sql(query);
            query = "";

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
