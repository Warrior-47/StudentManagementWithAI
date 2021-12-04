using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementWithAI.Migrations
{
    public partial class addedCoursesOfferedToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Course_PreReq",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "PreReq",
                table: "Course",
                newName: "PreReqId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_PreReq",
                table: "Course",
                newName: "IX_Course_PreReqId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Course_PreReqId",
                table: "Course",
                column: "PreReqId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Course_PreReqId",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "PreReqId",
                table: "Course",
                newName: "PreReq");

            migrationBuilder.RenameIndex(
                name: "IX_Course_PreReqId",
                table: "Course",
                newName: "IX_Course_PreReq");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Course_PreReq",
                table: "Course",
                column: "PreReq",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
