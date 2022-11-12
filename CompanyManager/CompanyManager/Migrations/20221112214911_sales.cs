using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Migrations
{
    public partial class sales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Sale_SaleId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Person_SellerId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_SellerId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Product_SaleId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    UnitPrice = table.Column<float>(type: "REAL", nullable: false),
                    SaleId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCart_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCart_SaleId",
                table: "ProductCart",
                column: "SaleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCart");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Sale",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Product",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_SellerId",
                table: "Sale",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SaleId",
                table: "Product",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Sale_SaleId",
                table: "Product",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Person_SellerId",
                table: "Sale",
                column: "SellerId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
