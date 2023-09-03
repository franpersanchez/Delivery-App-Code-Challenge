using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class add_more_data_in_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Apellidos", "Email", "Nombre", "Telefono", "Ubicacion_latitud", "Ubicacion_longitud" },
                values: new object[] { 2L, "Acedo", "martaab@gmail.com", "Marta", "+34 665412984", "1232132N", "324E" });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Id", "Matricula", "NombreConductor" },
                values: new object[,]
                {
                    { 1L, "418GZK", "Ivan Ruiz" },
                    { 2L, "345HJU", "Lolo Sanchez" }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "ClienteId", "Commentarios", "EnvioId", "EstadoPedido", "HoraCreacion", "VehiculoId" },
                values: new object[,]
                {
                    { 2L, 2L, "El corte ingles", null, 0, "2023-09-01-15:55", null },
                    { 3L, 2L, "Fnac", null, 0, "2023-08-03-23:55", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
