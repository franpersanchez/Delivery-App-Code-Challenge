using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class Third_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ubicacion_UbicacionId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_historialUbicaciones_Ubicacion_UbicacionId",
                table: "historialUbicaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_historialUbicaciones",
                table: "historialUbicaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ubicacion",
                table: "Ubicacion");

            migrationBuilder.RenameTable(
                name: "historialUbicaciones",
                newName: "HistorialUbicaciones");

            migrationBuilder.RenameTable(
                name: "Ubicacion",
                newName: "Ubicaciones");

            migrationBuilder.RenameIndex(
                name: "IX_historialUbicaciones_UbicacionId",
                table: "HistorialUbicaciones",
                newName: "IX_HistorialUbicaciones_UbicacionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistorialUbicaciones",
                table: "HistorialUbicaciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ubicaciones",
                table: "Ubicaciones",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UbicacionesVehiculo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehiculoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaUltimaUbicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UbicacionActualId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionesVehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionesVehiculo_Ubicaciones_UbicacionActualId",
                        column: x => x.UbicacionActualId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionesVehiculo_UbicacionActualId",
                table: "UbicacionesVehiculo",
                column: "UbicacionActualId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ubicaciones_UbicacionId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_HistorialUbicaciones_Ubicaciones_UbicacionId",
                table: "HistorialUbicaciones");

            migrationBuilder.DropTable(
                name: "UbicacionesVehiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistorialUbicaciones",
                table: "HistorialUbicaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ubicaciones",
                table: "Ubicaciones");

            migrationBuilder.RenameTable(
                name: "HistorialUbicaciones",
                newName: "historialUbicaciones");

            migrationBuilder.RenameTable(
                name: "Ubicaciones",
                newName: "Ubicacion");

            migrationBuilder.RenameIndex(
                name: "IX_HistorialUbicaciones_UbicacionId",
                table: "historialUbicaciones",
                newName: "IX_historialUbicaciones_UbicacionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_historialUbicaciones",
                table: "historialUbicaciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ubicacion",
                table: "Ubicacion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Ubicacion_UbicacionId",
                table: "Clientes",
                column: "UbicacionId",
                principalTable: "Ubicacion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_historialUbicaciones_Ubicacion_UbicacionId",
                table: "historialUbicaciones",
                column: "UbicacionId",
                principalTable: "Ubicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
