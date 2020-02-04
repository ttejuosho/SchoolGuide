using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolGuide.Migrations
{
    public partial class AddProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolName = table.Column<string>(nullable: false),
                    SchoolAddress = table.Column<string>(nullable: false),
                    SchoolCity = table.Column<string>(nullable: false),
                    SchoolState = table.Column<string>(nullable: false),
                    PrincipalName = table.Column<string>(nullable: false),
                    SchoolPhone = table.Column<string>(nullable: false),
                    SchoolPhone2 = table.Column<string>(nullable: true),
                    SchoolEmail = table.Column<string>(nullable: false),
                    SchoolWeb = table.Column<string>(nullable: true),
                    IsReligious = table.Column<bool>(nullable: false),
                    IsBoarding = table.Column<bool>(nullable: false),
                    ProfileImagePath = table.Column<string>(nullable: true),
                    Established = table.Column<string>(nullable: true),
                    GovtApproved = table.Column<bool>(nullable: false),
                    NumberOfStudents = table.Column<int>(nullable: false),
                    NumberOfTeachers = table.Column<int>(nullable: false),
                    NumberOfCampuses = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
