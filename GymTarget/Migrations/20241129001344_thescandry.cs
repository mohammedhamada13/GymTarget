using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTarget.Migrations
{
    /// <inheritdoc />
    public partial class thescandry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "coaches");
        }
    }
}
