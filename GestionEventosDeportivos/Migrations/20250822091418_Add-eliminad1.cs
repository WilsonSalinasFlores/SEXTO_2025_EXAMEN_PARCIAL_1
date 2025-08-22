using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEventosDeportivos.Migrations
{
    /// <inheritdoc />
    public partial class Addeliminad1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Paticipantes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Eventos",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Paticipantes");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Eventos");
        }
    }
}
