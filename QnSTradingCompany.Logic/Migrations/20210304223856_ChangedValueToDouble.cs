using Microsoft.EntityFrameworkCore.Migrations;

namespace QnSTradingCompany.Logic.Migrations
{
    public partial class ChangedValueToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                schema: "App",
                table: "Condition",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                schema: "App",
                table: "Condition",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
