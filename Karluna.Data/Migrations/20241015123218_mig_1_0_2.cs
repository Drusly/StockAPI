using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karluna.Data.Migrations
{
    public partial class mig_1_0_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StockProductVersions",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "StockProductVersions");
        }
    }
}
