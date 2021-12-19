using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementWithAI.Migrations
{
    public partial class addedWeekDaysToCoursesOffered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeekDays",
                table: "CoursesOffered",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekDays",
                table: "CoursesOffered");
        }
    }
}
