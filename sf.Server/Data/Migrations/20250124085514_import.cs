using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sf.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class import : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Imported",
                table: "Entity<Guid>",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imported",
                table: "Entity<Guid>");
        }
    }
}
