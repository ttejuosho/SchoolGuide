using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolGuide.Migrations
{
    public partial class schoolDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Established",
                table: "Schools");

            migrationBuilder.AddColumn<string>(
                name: "AgeRange",
                table: "Schools",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "Schools",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SchoolDescription",
                table: "Schools",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "YearFounded",
                table: "Schools",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "SchoolDescription",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "YearFounded",
                table: "Schools");

            migrationBuilder.AddColumn<string>(
                name: "Established",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
