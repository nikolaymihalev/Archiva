using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archiva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConstants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "User last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "User last name");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "User first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "User first name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Documents",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "Document name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Document name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                comment: "User last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "User last name");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                comment: "User first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "User first name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Document name",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "Document name");
        }
    }
}
