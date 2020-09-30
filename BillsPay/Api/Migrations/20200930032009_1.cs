using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Multa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PeriodoMaximoDeDiasEmAtraso = table.Column<int>(nullable: true),
                    PorcentagemAplicadaDiaAtraso = table.Column<double>(nullable: false),
                    PorcentagemAplicadaMulta = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaAPagar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    ValorOriginal = table.Column<decimal>(nullable: false),
                    ValorCorrigido = table.Column<decimal>(nullable: false),
                    QuantidadeDiasEmAtraso = table.Column<int>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false),
                    MultaAplicadaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaAPagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaAPagar_Multa_MultaAplicadaId",
                        column: x => x.MultaAplicadaId,
                        principalTable: "Multa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Multa",
                columns: new[] { "Id", "PeriodoMaximoDeDiasEmAtraso", "PorcentagemAplicadaDiaAtraso", "PorcentagemAplicadaMulta" },
                values: new object[] { new Guid("557256ce-83b3-4892-9acd-c8b03ed6a9de"), 3, 0.001, 0.02 });

            migrationBuilder.InsertData(
                table: "Multa",
                columns: new[] { "Id", "PeriodoMaximoDeDiasEmAtraso", "PorcentagemAplicadaDiaAtraso", "PorcentagemAplicadaMulta" },
                values: new object[] { new Guid("6c9f40d0-89c4-4326-8b49-fa48cbaa521a"), 5, 0.002, 0.029999999999999999 });

            migrationBuilder.InsertData(
                table: "Multa",
                columns: new[] { "Id", "PeriodoMaximoDeDiasEmAtraso", "PorcentagemAplicadaDiaAtraso", "PorcentagemAplicadaMulta" },
                values: new object[] { new Guid("538aafe5-4edf-4e0a-a629-67e36a1a11cb"), null, 0.0030000000000000001, 0.050000000000000003 });

            migrationBuilder.CreateIndex(
                name: "IX_ContaAPagar_MultaAplicadaId",
                table: "ContaAPagar",
                column: "MultaAplicadaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaAPagar");

            migrationBuilder.DropTable(
                name: "Multa");
        }
    }
}
