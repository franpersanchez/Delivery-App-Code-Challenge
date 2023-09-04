using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class fix_incorrect_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "Pedidos");

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HoraCreacion",
                value: new DateTime(2023, 9, 4, 11, 9, 56, 661, DateTimeKind.Utc).AddTicks(5259));

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2L,
                column: "HoraCreacion",
                value: new DateTime(2023, 9, 4, 11, 9, 56, 661, DateTimeKind.Utc).AddTicks(5261));

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3L,
                column: "HoraCreacion",
                value: new DateTime(2023, 9, 4, 11, 9, 56, 661, DateTimeKind.Utc).AddTicks(5263));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VehiculoId",
                table: "Pedidos",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "HoraCreacion", "VehiculoId" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 22, 29, 178, DateTimeKind.Utc).AddTicks(5370), null });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "HoraCreacion", "VehiculoId" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 22, 29, 178, DateTimeKind.Utc).AddTicks(5372), null });

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "HoraCreacion", "VehiculoId" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 22, 29, 178, DateTimeKind.Utc).AddTicks(5373), null });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_VehiculoId",
                table: "Pedidos",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id");
        }
    }
}
