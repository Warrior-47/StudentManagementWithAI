using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementWithAI.Migrations
{
    public partial class changedCoursesOfferedCourseTaken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTaken_Course_CourseId",
                table: "CourseTaken");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTaken_Faculty_FacultyId",
                table: "CourseTaken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTaken",
                table: "CourseTaken");

            migrationBuilder.DropIndex(
                name: "IX_CourseTaken_CourseId",
                table: "CourseTaken");

            migrationBuilder.DropIndex(
                name: "IX_CourseTaken_FacultyId",
                table: "CourseTaken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesOffered",
                table: "CoursesOffered");

            migrationBuilder.DropColumn(
                name: "ScheduledTime",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTaken",
                table: "CourseTaken",
                columns: new[] { "StudentId", "FacultyId", "CourseId", "Section" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesOffered",
                table: "CoursesOffered",
                columns: new[] { "FacultyId", "CourseId", "Section" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTaken_CoursesOffered_CoursesOfferedFacultyId_CoursesOfferedCourseId_CoursesOfferedSection",
                table: "CourseTaken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTaken",
                table: "CourseTaken");

            migrationBuilder.DropIndex(
                name: "IX_CourseTaken_CoursesOfferedFacultyId_CoursesOfferedCourseId_CoursesOfferedSection",
                table: "CourseTaken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesOffered",
                table: "CoursesOffered");

            migrationBuilder.DropColumn(
                name: "CoursesOfferedCourseId",
                table: "CourseTaken");

            migrationBuilder.DropColumn(
                name: "CoursesOfferedFacultyId",
                table: "CourseTaken");

            migrationBuilder.DropColumn(
                name: "CoursesOfferedSection",
                table: "CourseTaken");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledTime",
                table: "CourseTaken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTaken",
                table: "CourseTaken",
                columns: new[] { "StudentId", "FacultyId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesOffered",
                table: "CoursesOffered",
                columns: new[] { "FacultyId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTaken_CourseId",
                table: "CourseTaken",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTaken_FacultyId",
                table: "CourseTaken",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTaken_Course_CourseId",
                table: "CourseTaken",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTaken_Faculty_FacultyId",
                table: "CourseTaken",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
