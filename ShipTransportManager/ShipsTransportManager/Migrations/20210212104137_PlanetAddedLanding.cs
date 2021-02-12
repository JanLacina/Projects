using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipsTransportManager.Migrations
{
    public partial class PlanetAddedLanding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Landing",
                table: "Planets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Landing",
                table: "Planets");
        }
    }
}
