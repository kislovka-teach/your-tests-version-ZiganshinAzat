using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Login", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { "user1", "User1", "password1", 0 },
                    { "user2", "User2", "password2", 1 },
                    { "user3", "User3", "password3", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user1");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user2");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user3");
        }
    }
}
