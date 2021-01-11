using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations.ApplicationDb
{
    public partial class NewsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_News_1",
                table: "News_1");

            migrationBuilder.RenameTable(
                name: "News_1",
                newName: "News");

            migrationBuilder.AddPrimaryKey(
                name: "PK_News",
                table: "News",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_News",
                table: "News");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "News_1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_News_1",
                table: "News_1",
                column: "NewsId");
        }
    }
}
