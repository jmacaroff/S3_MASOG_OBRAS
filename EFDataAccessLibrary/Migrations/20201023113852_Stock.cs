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
            query = query + "(9, N'Tel-Fix', 23923494739, NULL, NULL, NULL, N'Tornillos') ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Proveedores] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Ordenes] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Ordenes] ([Id], [ProveedorId], [Fecha], [FechaEntrega], [Observacion]) VALUES  ";
            query = query + "(12, 3, CAST(N'2020-09-16T00:00:00.0000000' AS DateTime2), CAST(N'2020-09-30T00:00:00.0000000' AS DateTime2), N'Entrega demorada p/ cuarentena'), ";
            query = query + "(14, 9, CAST(N'2020-09-30T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-07T00:00:00.0000000' AS DateTime2), N'Entrega a tiempo'), ";
            query = query + "(17, 7, CAST(N'2020-10-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-01T00:00:00.0000000' AS DateTime2), N'Pasaje Braga'), ";
            query = query + "(18, 4, CAST(N'2020-10-02T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-02T00:00:00.0000000' AS DateTime2), N'Flete Tel-Fix Ord. 14'), ";
            query = query + "(19, 9, CAST(N'2020-10-02T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-11T00:00:00.0000000' AS DateTime2), N'Entrega a tiempo'), ";
            query = query + "(20, 4, CAST(N'2020-10-04T00:00:00.0000000' AS DateTime2), CAST(N'2020-10-04T00:00:00.0000000' AS DateTime2), N'Flete Tel-Fix Ord. 19') ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Ordenes] OFF ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[OrdenItems] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[OrdenItems] ([Id], [OrdenId], [ProductoId], [Cantidad], [Precio], [Observacion]) VALUES  ";
            query = query + "(3, 12, N'DUR001', 100, CAST(970.45 AS Decimal(18, 2)), NULL), ";
            query = query + "(4, 12, N'DUR002', 80, CAST(1020.80 AS Decimal(18, 2)), NULL), ";
            query = query + "(6, 14, N'TOR001', 5000, CAST(9.00 AS Decimal(18, 2)), NULL), ";
            query = query + "(7, 14, N'TOR002', 10000, CAST(11.80 AS Decimal(18, 2)), NULL), ";
            query = query + "(9, 14, N'TOR101', 10000, CAST(9.45 AS Decimal(18, 2)), NULL), ";
            query = query + "(10, 17, N'PAS001', 1, CAST(6890.58 AS Decimal(18, 2)), NULL), ";
            query = query + "(12, 18, N'FLT001', 1, CAST(2600.00 AS Decimal(18, 2)), NULL), ";
            query = query + "(13, 19, N'TOR002', 20000, CAST(9.34 AS Decimal(18, 2)), NULL), ";
            query = query + "(15, 19, N'TOR001', 15000, CAST(8.92 AS Decimal(18, 2)), NULL), ";
            query = query + "(16, 20, N'FLT001', 1, CAST(2650.00 AS Decimal(18, 2)), NULL) ";

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
            query = query + "(9, N'Gutierrez Hermanos', 30575872832, NULL, 3852548448, NULL), ";
            query = query + "(10, N'Adolfo Alsina', 18564762, NULL, NULL, N'aalsina45@gmail.com') ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "SET IDENTITY_INSERT [dbo].[Clientes] OFF ";

            migrationBuilder.Sql(query);
            query = "";

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

            query = query + "SET IDENTITY_INSERT [dbo].[Proyectos] ON  ";

            migrationBuilder.Sql(query);
            query = "";

            query = query + "INSERT [dbo].[Proyectos] ([Id], [ClienteId], [Nombre], [Direccion], [Observacion]) VALUES  ";
            query = query + "(1, 1, N'Vivienda Braga', N'Av. de Los Incas 3232', NULL), ";
            query = query + "(3, 4, N'Casa El Tipal Urribarri', N'Las Tipas casa 78', N'Fecha finalización estipulada 30/04/2021'), ";
            query = query + "(4, 8, N'Ampliación local La Paulina', N'Reyes Católicos 456', NULL) ";

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
            query = query + "AS BEGIN ";
            query = query + "DECLARE @MovStockId int ";
            query = query + "DECLARE @DepositoId nchar(3) ";
            query = query + "DECLARE @ProductoId varchar(6) ";
            query = query + "DECLARE @Cantidad int ";
            query = query + "SET @MovStockId = (SELECT MovStockId from inserted) ";
            query = query + "SET @DepositoId = (SELECT DepositoId from MovsStock ";
            query = query + "                   where Id = @MovStockId) ";
            query = query + "SET @ProductoId = (SELECT ProductoId from inserted) ";
            query = query + "BEGIN ";
            query = query + "IF(SELECT EsEgreso from TipoMovimiento where Id = (SELECT TipoMovimientoId from MovsStock where Id = @MovStockId)) = 0 ";
            query = query + "   BEGIN ";
            query = query + "   SET @Cantidad = (SELECT Cantidad from inserted) ";
            query = query + "   END ";
            query = query + "ELSE ";
            query = query + "   BEGIN ";
            query = query + "   SET @Cantidad = (SELECT(Cantidad * (-1)) from inserted) ";
            query = query + "   END ";
            query = query + "END ";
            query = query + "BEGIN ";
            query = query + "IF(SELECT Cantidad from Stock Where ProductoId = @ProductoId and DepositoId = @DepositoId) is null ";
            query = query + "    BEGIN ";
            query = query + "    BEGIN TRAN ";
            query = query + "    INSERT INTO Stock(ProductoId, DepositoId, Cantidad) ";
            query = query + "    VALUES(@ProductoId, @DepositoId, @Cantidad) ";
            query = query + "    COMMIT TRAN ";
            query = query + "    END ";
            query = query + "ELSE ";
            query = query + "    BEGIN ";
            query = query + "    BEGIN TRAN ";
            query = query + "    UPDATE STOCK SET Cantidad = Cantidad + @Cantidad ";
            query = query + "    WHERE DepositoId = @DepositoId and ProductoId = @ProductoId ";
            query = query + "    COMMIT TRAN ";
            query = query + "    END ";
            query = query + "END ";
            query = query + "END ";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
