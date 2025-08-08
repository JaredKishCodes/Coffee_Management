using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCoffeeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoffeeName",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoffeeName",
                table: "CartItems");
        }
    }
}
