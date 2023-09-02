using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class migrationwhaterver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "HistorialUbicaciones");

            migrationBuilder.DropTable(
                name: "UbicacionesVehiculo");

            migrationBuilder.DropTable(
                name: "UbicacionVehiculoCoordenadas");

            migrationBuilder.DropColumn(
                name: "Aceptado",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Entregado",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Pagado",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "longitud",
                table: "Clientes",
                newName: "Ubicacion_longitud");

            migrationBuilder.RenameColumn(
                name: "latitud",
                table: "Clientes",
                newName: "Ubicacion_latitud");

            migrationBuilder.AlterColumn<long>(
                name: "VehiculoId",
                table: "Pedidos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstadoPedido",
                table: "Pedidos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegistroUbicacion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ubicacion_longitud = table.Column<string>(type: "text", nullable: false),
                    Ubicacion_latitud = table.Column<string>(type: "text", nullable: false),
                    VehiculoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroUbicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroUbicacion_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroUbicacion_VehiculoId",
                table: "RegistroUbicacion",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "RegistroUbicacion");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "EstadoPedido",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Ubicacion_longitud",
                table: "Clientes",
                newName: "longitud");

            migrationBuilder.RenameColumn(
                name: "Ubicacion_latitud",
                table: "Clientes",
                newName: "latitud");

            migrationBuilder.AlterColumn<long>(
                name: "VehiculoId",
                table: "Pedidos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "Aceptado",
                table: "Pedidos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Entregado",
                table: "Pedidos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Pagado",
                table: "Pedidos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateTable(
                name: "HistorialUbicaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UbicacionVehiculoCoordenadasId = table.Column<long>(type: "bigint", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialUbicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialUbicaciones_UbicacionVehiculoCoordenadas_Ubicacion~",
                        column: x => x.UbicacionVehiculoCoordenadasId,
                        principalTable: "UbicacionVehiculoCoordenadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionesVehiculo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UbicacionVehiculoCoordenadasId = table.Column<long>(type: "bigint", nullable: false),
                    FechaUltimaUbicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VehiculoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionesVehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionesVehiculo_UbicacionVehiculoCoordenadas_UbicacionV~",
                        column: x => x.UbicacionVehiculoCoordenadasId,
                        principalTable: "UbicacionVehiculoCoordenadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialUbicaciones_UbicacionVehiculoCoordenadasId",
                table: "HistorialUbicaciones",
                column: "UbicacionVehiculoCoordenadasId");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionesVehiculo_UbicacionVehiculoCoordenadasId",
                table: "UbicacionesVehiculo",
                column: "UbicacionVehiculoCoordenadasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id");
        }
    }
}
