using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "CoffeeItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "CoffeeItems");
        }
    }
}
