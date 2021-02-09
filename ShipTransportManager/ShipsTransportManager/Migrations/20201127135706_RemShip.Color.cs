using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipsTransportManager.Migrations
{
    public partial class RemShipColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Ships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Ships",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
