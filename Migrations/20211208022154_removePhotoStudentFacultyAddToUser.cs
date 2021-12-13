using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementWithAI.Migrations
{
    public partial class removePhotoStudentFacultyAddToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Faculty");

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Faculty",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
