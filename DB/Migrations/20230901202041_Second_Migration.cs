using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class Second_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_historialUbicaciones_Vehiculos_VehiculoId",
                table: "historialUbicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_historialUbicaciones_VehiculoId",
                table: "historialUbicaciones");

            migrationBuilder.DropColumn(
                name: "UbicacionActual",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "historialUbicaciones");

            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "historialUbicaciones");

            migrationBuilder.DropColumn(
                name: "ubicacion",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "historialUbicaciones",
                newName: "FechaRegistro");

            migrationBuilder.RenameColumn(
                name: "telefono",
                table: "Clientes",
                newName: "Telefono");

            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "Vehiculos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "NombreConductor",
                table: "Vehiculos",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Commentarios",
                table: "Pedidos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraCreacion",
                table: "Pedidos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "VehiculoId",
                table: "Pedidos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UbicacionId",
                table: "historialUbicaciones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UbicacionId",
                table: "Clientes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitud = table.Column<float>(type: "real", nullable: false),
                    longitud = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_VehiculoId",
                table: "Pedidos",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_historialUbicaciones_UbicacionId",
                table: "historialUbicaciones",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UbicacionId",
                table: "Clientes",
                column: "UbicacionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ubicacion_UbicacionId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_historialUbicaciones_Ubicacion_UbicacionId",
                table: "historialUbicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "Ubicacion");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_historialUbicaciones_UbicacionId",
                table: "historialUbicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UbicacionId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NombreConductor",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "HoraCreacion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "historialUbicaciones");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "historialUbicaciones",
                newName: "FechaHora");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Clientes",
                newName: "telefono");

            migrationBuilder.AlterColumn<string>(
                name: "Matricula",
                table: "Vehiculos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UbicacionActual",
                table: "Vehiculos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Commentarios",
                table: "Pedidos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "historialUbicaciones",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "VehiculoId",
                table: "historialUbicaciones",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ubicacion",
                table: "Clientes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_historialUbicaciones_VehiculoId",
                table: "historialUbicaciones",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_historialUbicaciones_Vehiculos_VehiculoId",
                table: "historialUbicaciones",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
