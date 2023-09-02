using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class fourthmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ubicaciones_UbicacionId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_HistorialUbicaciones_Ubicaciones_UbicacionId",
                table: "HistorialUbicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_UbicacionesVehiculo_Ubicaciones_UbicacionActualId",
                table: "UbicacionesVehiculo");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UbicacionId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "UbicacionActualId",
                table: "UbicacionesVehiculo",
                newName: "UbicacionVehiculoCoordenadasId");

            migrationBuilder.RenameIndex(
                name: "IX_UbicacionesVehiculo_UbicacionActualId",
                table: "UbicacionesVehiculo",
                newName: "IX_UbicacionesVehiculo_UbicacionVehiculoCoordenadasId");

            migrationBuilder.RenameColumn(
                name: "UbicacionId",
                table: "HistorialUbicaciones",
                newName: "UbicacionVehiculoCoordenadasId");

            migrationBuilder.RenameIndex(
                name: "IX_HistorialUbicaciones_UbicacionId",
                table: "HistorialUbicaciones",
                newName: "IX_HistorialUbicaciones_UbicacionVehiculoCoordenadasId");

            migrationBuilder.AddColumn<float>(
                name: "latitud",
                table: "Clientes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "longitud",
                table: "Clientes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "UbicacionVehiculoCoordenadas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitud = table.Column<float>(type: "real", nullable: false),
                    longitud = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionVehiculoCoordenadas", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialUbicaciones_UbicacionVehiculoCoordenadas_Ubicacion~",
                table: "HistorialUbicaciones",
                column: "UbicacionVehiculoCoordenadasId",
                principalTable: "UbicacionVehiculoCoordenadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UbicacionesVehiculo_UbicacionVehiculoCoordenadas_UbicacionV~",
                table: "UbicacionesVehiculo",
                column: "UbicacionVehiculoCoordenadasId",
                principalTable: "UbicacionVehiculoCoordenadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialUbicaciones_UbicacionVehiculoCoordenadas_Ubicacion~",
                table: "HistorialUbicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_UbicacionesVehiculo_UbicacionVehiculoCoordenadas_UbicacionV~",
                table: "UbicacionesVehiculo");

            migrationBuilder.DropTable(
                name: "UbicacionVehiculoCoordenadas");

            migrationBuilder.DropColumn(
                name: "latitud",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "longitud",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "UbicacionVehiculoCoordenadasId",
                table: "UbicacionesVehiculo",
                newName: "UbicacionActualId");

            migrationBuilder.RenameIndex(
                name: "IX_UbicacionesVehiculo_UbicacionVehiculoCoordenadasId",
                table: "UbicacionesVehiculo",
                newName: "IX_UbicacionesVehiculo_UbicacionActualId");

            migrationBuilder.RenameColumn(
                name: "UbicacionVehiculoCoordenadasId",
                table: "HistorialUbicaciones",
                newName: "UbicacionId");

            migrationBuilder.RenameIndex(
                name: "IX_HistorialUbicaciones_UbicacionVehiculoCoordenadasId",
                table: "HistorialUbicaciones",
                newName: "IX_HistorialUbicaciones_UbicacionId");

            migrationBuilder.AddColumn<long>(
                name: "UbicacionId",
                table: "Clientes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitud = table.Column<float>(type: "real", nullable: false),
                    longitud = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UbicacionId",
                table: "Clientes",
                column: "UbicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Ubicaciones_UbicacionId",
                table: "Clientes",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialUbicaciones_Ubicaciones_UbicacionId",
                table: "HistorialUbicaciones",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UbicacionesVehiculo_Ubicaciones_UbicacionActualId",
                table: "UbicacionesVehiculo",
                column: "UbicacionActualId",
                principalTable: "Ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
