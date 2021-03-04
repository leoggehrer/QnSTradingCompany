using Microsoft.EntityFrameworkCore.Migrations;

namespace QnSTradingCompany.Logic.Migrations
{
    public partial class AddedQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                schema: "App",
                table: "Condition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "App",
                table: "Condition");
        }
    }
}
