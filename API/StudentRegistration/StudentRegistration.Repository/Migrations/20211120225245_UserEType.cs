using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentRegistration.Repository.Migrations
{
    public partial class UserEType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EType",
                table: "Users");
        }
    }
}
