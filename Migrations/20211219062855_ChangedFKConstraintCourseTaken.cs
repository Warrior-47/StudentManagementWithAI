using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementWithAI.Migrations
{
    public partial class ChangedFKConstraintCourseTaken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTaken_CoursesOffered_CoursesOfferedFacultyId_CoursesOfferedCourseId_CoursesOfferedSection",
                table: "CourseTaken");

            migrationBuilder.DropIndex(
                name: "IX_CourseTaken_CoursesOfferedFacultyId_CoursesOfferedCourseId_CoursesOfferedSection",
                table: "CourseTaken");

            migrationBuilder.DropColumn(
                name: "CoursesOfferedCourseId",
                table: "CourseTaken");

            migrationBuilder.DropColumn(
                name: "CoursesOfferedFacultyId",
                table: "CourseTaken");

            migrationBuilder.DropColumn(
                name: "CoursesOfferedSection",
                table: "CourseTaken");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTaken_FacultyId_CourseId_Section",
                table: "CourseTaken",
                columns: new[] { "FacultyId", "CourseId", "Section" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTaken_CoursesOffered_FacultyId_CourseId_Section",
                table: "CourseTaken",
                columns: new[] { "FacultyId", "CourseId", "Section" },
                principalTable: "CoursesOffered",
                principalColumns: new[] { "FacultyId", "CourseId", "Section" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTaken_CoursesOffered_FacultyId_CourseId_Section",
                table: "CourseTaken");

            migrationBuilder.DropIndex(
                name: "IX_CourseTaken_FacultyId_CourseId_Section",
                table: "CourseTaken");

            migrationBuilder.AddColumn<int>(
                name: "CoursesOfferedCourseId",
                table: "CourseTaken",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoursesOfferedFacultyId",
                table: "CourseTaken",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoursesOfferedSection",
                table: "CourseTaken",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTaken_CoursesOfferedFacultyId_CoursesOfferedCourseId_CoursesOfferedSection",
                table: "CourseTaken",
                columns: new[] { "CoursesOfferedFacultyId", "CoursesOfferedCourseId", "CoursesOfferedSection" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTaken_CoursesOffered_CoursesOfferedFacultyId_CoursesOfferedCourseId_CoursesOfferedSection",
                table: "CourseTaken",
                columns: new[] { "CoursesOfferedFacultyId", "CoursesOfferedCourseId", "CoursesOfferedSection" },
                principalTable: "CoursesOffered",
                principalColumns: new[] { "FacultyId", "CourseId", "Section" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
