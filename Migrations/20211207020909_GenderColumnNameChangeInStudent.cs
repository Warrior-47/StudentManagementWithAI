using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementWithAI.Migrations
{
    public partial class GenderColumnNameChangeInStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Student",
                newName: "Gender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Student",
                newName: "gender");
        }
    }
}
