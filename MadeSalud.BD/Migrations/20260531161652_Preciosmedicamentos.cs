using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadeSalud.BD.Migrations
{
    /// <inheritdoc />
    public partial class Preciosmedicamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio15",
                table: "Medicamentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio25",
                table: "Medicamentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioAmaCasa30",
                table: "Medicamentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "Medicamentos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Presentacion",
                table: "Medicamentos",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "Precio15",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "Precio25",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "PrecioAmaCasa30",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "Presentacion",
                table: "Medicamentos");
        }
    }
}
