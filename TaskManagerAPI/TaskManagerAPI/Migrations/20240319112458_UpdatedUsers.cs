using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user1",
                column: "Password",
                value: "434F30F6A76EF55ECAE03197BEC4A3F14CA29DE9979E3A84D6B6A93857B6BAE4:FEFF4E79FB775715E64947DD1B5A4822:50000:SHA256");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user2",
                column: "Password",
                value: "FBAAD6BBE5DFCF68420FA28FB05BEB6B2BE0563D59314A18E2E440F4D25E2429:9BCB8CA1DB63344126C5A316DA1459C4:50000:SHA256");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user3",
                column: "Password",
                value: "4A7353495B9C92C235A2BAFFBBE368125953FF25AB6513B2D610D0AFAA437244:03170422FDDAB4ECA63D08A92A59D564:50000:SHA256");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user1",
                column: "Password",
                value: "password1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user2",
                column: "Password",
                value: "password2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Login",
                keyValue: "user3",
                column: "Password",
                value: "password3");
        }
    }
}
