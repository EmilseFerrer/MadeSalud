using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadeSalud.BD.Migrations
{
    /// <inheritdoc />
    public partial class NombreFormulaycategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "NombreFormula",
                table: "Medicamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NombreFormula",
                table: "Medicamentos",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }
    }
}
