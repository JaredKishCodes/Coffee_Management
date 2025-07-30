using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCoffeeIdFromCoffeeCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoffeeId",
                table: "CoffeeCategories");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "CoffeeItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "CoffeeItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CoffeeId",
                table: "CoffeeCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
