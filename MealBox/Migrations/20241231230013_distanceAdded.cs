using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealBox.Migrations
{
    /// <inheritdoc />
    public partial class distanceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Products",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Products");
        }
    }
}
