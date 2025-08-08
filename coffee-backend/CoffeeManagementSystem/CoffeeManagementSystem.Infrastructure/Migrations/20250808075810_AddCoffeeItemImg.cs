using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCoffeeItemImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoffeeItemImg",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoffeeItemImg",
                table: "CartItems");
        }
    }
}
