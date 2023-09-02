using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class change_pedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<long>(
                name: "VehiculoId",
                table: "Pedidos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<long>(
                name: "VehiculoId",
                table: "Pedidos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Vehiculos_VehiculoId",
                table: "Pedidos",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
