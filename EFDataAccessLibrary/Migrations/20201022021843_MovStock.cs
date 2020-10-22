using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccessLibrary.Migrations
{
    public partial class MovStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoMovimiento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EsEgreso = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovsStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMovimientoId = table.Column<int>(nullable: false),
                    DepositoId = table.Column<string>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: true),
                    ProveedorId = table.Column<int>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovsStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovsStock_Depositos_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Depositos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovsStock_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovsStock_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovsStock_TipoMovimiento_TipoMovimientoId",
                        column: x => x.TipoMovimientoId,
                        principalTable: "TipoMovimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovStockItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovStockId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<string>(type: "varchar(6)", nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovStockItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovStockItems_MovsStock_MovStockId",
                        column: x => x.MovStockId,
                        principalTable: "MovsStock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovStockItems_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovsStock_DepositoId",
                table: "MovsStock",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovsStock_ProveedorId",
                table: "MovsStock",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovsStock_ProyectoId",
                table: "MovsStock",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovsStock_TipoMovimientoId",
                table: "MovsStock",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovStockItems_MovStockId",
                table: "MovStockItems",
                column: "MovStockId");

            migrationBuilder.CreateIndex(
                name: "IX_MovStockItems_ProductoId",
                table: "MovStockItems",
                column: "ProductoId");

            string query = "";

            query = query + "INSERT [dbo].[TipoMovimiento] ([EsEgreso], [Descripcion]) " +
                "VALUES (0, 'Ingreso por Ajuste'), (0, 'Remito de Proveedor'), (0, 'Devolución de Cliente')," +
                "(1, 'Egreso por Ajuste'), (1, 'Remito a Obra'), (1, 'Material Defectuoso')";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovStockItems");

            migrationBuilder.DropTable(
                name: "MovsStock");

            migrationBuilder.DropTable(
                name: "TipoMovimiento");
        }
    }
}
