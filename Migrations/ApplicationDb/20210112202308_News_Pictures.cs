using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations.ApplicationDb
{
    public partial class News_Pictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicUrl",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicUrl",
                table: "News");
        }
    }
}
