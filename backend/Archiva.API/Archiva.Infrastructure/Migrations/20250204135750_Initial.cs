using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Archiva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Document identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Document name"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Document image"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Document description"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Document issue date"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Document end date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                },
                comment: "Represents the Document");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User identifier"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "User first name"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "User last name"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "User email"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "User password")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                },
                comment: "Represents the User");

            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User identifier"),
                    DocumentId = table.Column<int>(type: "int", nullable: false, comment: "Document identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => new { x.UserId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_UserDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents mapping between User and Document");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_DocumentId",
                table: "UserDocuments",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDocuments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
