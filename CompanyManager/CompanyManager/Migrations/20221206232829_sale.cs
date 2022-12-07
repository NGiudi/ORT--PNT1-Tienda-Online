using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Migrations
{
    public partial class sale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardCVV",
                table: "Sale",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CardName",
                table: "Sale",
                type: "TEXT",
                maxLength: 18,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Sale",
                type: "TEXT",
                maxLength: 18,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExpirationM",
                table: "Sale",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpirationY",
                table: "Sale",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardCVV",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "CardName",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "ExpirationM",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "ExpirationY",
                table: "Sale");
        }
    }
}
