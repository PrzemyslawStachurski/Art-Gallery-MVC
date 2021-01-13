using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations.ApplicationDb
{
    public partial class priceDecimalNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ArtPiece",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "ArtPiece",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
