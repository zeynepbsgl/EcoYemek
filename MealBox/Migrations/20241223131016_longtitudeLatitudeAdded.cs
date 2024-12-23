using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealBox.Migrations
{
    /// <inheritdoc />
    public partial class longtitudeLatitudeAdded : Migration

    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Products",
                type: "decimal(9,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Products",
                type: "decimal(9,6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Products");
        }
    }
}
