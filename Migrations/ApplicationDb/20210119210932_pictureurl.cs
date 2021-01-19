using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations.ApplicationDb
{
    public partial class pictureurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "News",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "News",
                maxLength: 70,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "News");
        }
    }
}
