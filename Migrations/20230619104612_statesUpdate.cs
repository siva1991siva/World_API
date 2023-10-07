using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace World.Api.Migrations
{
    /// <inheritdoc />
    public partial class statesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Population",
                table: "State",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Population",
                table: "State");
        }
    }
}
