using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations.ApplicationDb
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtPiece",
                columns: table => new
                {
                    ArtPieceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    AuthorsNote = table.Column<string>(nullable: true),
                    TypeOfArt = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    Reserved = table.Column<bool>(nullable: false),
                    PicUrl = table.Column<string>(nullable: true),
                    Horizontal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtPiece", x => x.ArtPieceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtPiece");
        }
    }
}
