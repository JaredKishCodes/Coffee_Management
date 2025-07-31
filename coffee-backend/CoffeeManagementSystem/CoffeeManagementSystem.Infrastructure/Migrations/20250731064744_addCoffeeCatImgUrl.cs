using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCoffeeCatImgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CoffeeCategories",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CoffeeCategories");
        }
    }
}
