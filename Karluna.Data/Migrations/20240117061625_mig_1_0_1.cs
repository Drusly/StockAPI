using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karluna.Data.Migrations
{
    public partial class mig_1_0_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "StockProducts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "StockProducts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockProducts_CategoryId",
                table: "StockProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockProducts_StockCategories_CategoryId",
                table: "StockProducts",
                column: "CategoryId",
                principalTable: "StockCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockProducts_StockCategories_CategoryId",
                table: "StockProducts");

            migrationBuilder.DropIndex(
                name: "IX_StockProducts_CategoryId",
                table: "StockProducts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "StockProducts");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "StockProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
