using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementWithAI.Migrations
{
    public partial class addedPhotoNameToFaculty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Faculty",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Faculty");
        }
    }
}
