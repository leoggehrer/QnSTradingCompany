using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QnSTradingCompany.Logic.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.EnsureSchema(
                name: "Data");

            migrationBuilder.EnsureSchema(
                name: "App");

            migrationBuilder.EnsureSchema(
                name: "MasterData");

            migrationBuilder.EnsureSchema(
                name: "Configuration");

            migrationBuilder.EnsureSchema(
                name: "Language");

            migrationBuilder.CreateTable(
                name: "BinaryData",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinaryData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(512)", maxLength: 512, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(512)", maxLength: 512, nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TimeOutInMinutes = table.Column<int>(type: "int", nullable: false),
                    EnableJwtAuth = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "MasterData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                schema: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeyLanguage = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ValueLanguage = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionLog",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionLog_Identity_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "Account",
                        principalTable: "Identity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoginSession",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityId = table.Column<int>(type: "int", nullable: false),
                    SessionToken = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OptionalInfo = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginSession_Identity_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "Account",
                        principalTable: "Identity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityId = table.Column<int>(type: "int", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Identity_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "Account",
                        principalTable: "Identity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ConditionType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condition_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "MasterData",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Condition_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "MasterData",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "MasterData",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "MasterData",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityXRole",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityXRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityXRole_Identity_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "Account",
                        principalTable: "Identity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityXRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Account",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLog_IdentityId",
                schema: "Account",
                table: "ActionLog",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_BinaryData_Guid",
                schema: "Data",
                table: "BinaryData",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Condition_CustomerId",
                schema: "App",
                table: "Condition",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Condition_ProductId",
                schema: "App",
                table: "Condition",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Number",
                schema: "MasterData",
                table: "Customer",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Email",
                schema: "Account",
                table: "Identity",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Name",
                schema: "Account",
                table: "Identity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityXRole_IdentityId",
                schema: "Account",
                table: "IdentityXRole",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityXRole_RoleId",
                schema: "Account",
                table: "IdentityXRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginSession_IdentityId",
                schema: "Account",
                table: "LoginSession",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "App",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductId",
                schema: "App",
                table: "Order",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Number",
                schema: "MasterData",
                table: "Product",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Designation",
                schema: "Account",
                table: "Role",
                column: "Designation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Setting_AppName_Key",
                schema: "Configuration",
                table: "Setting",
                columns: new[] { "AppName", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translation_AppName_KeyLanguage_Key",
                schema: "Language",
                table: "Translation",
                columns: new[] { "AppName", "KeyLanguage", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentityId",
                schema: "Account",
                table: "User",
                column: "IdentityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLog",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "BinaryData",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "Condition",
                schema: "App");

            migrationBuilder.DropTable(
                name: "IdentityXRole",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "LoginSession",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "App");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "Configuration");

            migrationBuilder.DropTable(
                name: "Translation",
                schema: "Language");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "MasterData");

            migrationBuilder.DropTable(
                name: "Identity",
                schema: "Account");
        }
    }
}
