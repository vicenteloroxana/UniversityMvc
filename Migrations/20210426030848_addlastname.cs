using Microsoft.EntityFrameworkCore.Migrations;

namespace InstituteWeb.Migrations
{
    public partial class addlastname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Last_Name",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Last_Name",
                table: "Students");
        }
    }
}
