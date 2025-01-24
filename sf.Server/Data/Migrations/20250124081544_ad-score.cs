using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sf.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class adscore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classes_ClassModelId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classes_ClassModelId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClassModelId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClassModelId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassModelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassModelId1",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Teams");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassModelId",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassModelId1",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassModelId",
                table: "Users",
                column: "ClassModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassModelId1",
                table: "Users",
                column: "ClassModelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_ClassModelId",
                table: "Users",
                column: "ClassModelId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_ClassModelId1",
                table: "Users",
                column: "ClassModelId1",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
