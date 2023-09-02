using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class add_Envios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EnvioId",
                table: "Pedidos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Envios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehiculoId = table.Column<long>(type: "bigint", nullable: false),
                    Entregado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envios_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EnvioId",
                table: "Pedidos",
                column: "EnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_VehiculoId",
                table: "Envios",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Envios_EnvioId",
                table: "Pedidos",
                column: "EnvioId",
                principalTable: "Envios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Envios_EnvioId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EnvioId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "EnvioId",
                table: "Pedidos");
        }
    }
}
