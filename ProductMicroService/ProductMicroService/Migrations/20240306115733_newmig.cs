using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductMicroService.Migrations
{
    public partial class newmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "AvailableQuantity",
                table: "Products",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "TotalQuantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "AvailableQuantity");
        }
    }
}
