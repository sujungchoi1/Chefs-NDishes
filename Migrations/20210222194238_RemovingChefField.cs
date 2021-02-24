using Microsoft.EntityFrameworkCore.Migrations;

namespace ChefAndDishes.Migrations
{
    public partial class RemovingChefField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chef",
                table: "Dishes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chef",
                table: "Dishes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
