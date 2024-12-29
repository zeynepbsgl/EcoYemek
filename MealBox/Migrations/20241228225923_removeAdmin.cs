using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealBox.Migrations
{
    /// <inheritdoc />
    public partial class removeAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Admins_AdminId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_AdminId",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_AdminId");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "Varchar(250)", maxLength: 250, nullable: true),
                    Authority = table.Column<string>(type: "Char(1)", maxLength: 1, nullable: true),
                    City = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: true),
                    DateOfBirth = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: true),
                    Gender = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: true),
                    Mail = table.Column<string>(type: "Varchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: false),
                    Province = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: true),
                    Surname = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Admins_AdminId",
                table: "Products",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
