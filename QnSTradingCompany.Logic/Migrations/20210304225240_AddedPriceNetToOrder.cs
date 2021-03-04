using Microsoft.EntityFrameworkCore.Migrations;

namespace QnSTradingCompany.Logic.Migrations
{
    public partial class AddedPriceNetToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceNet",
                schema: "App",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceNet",
                schema: "App",
                table: "Order");
        }
    }
}
