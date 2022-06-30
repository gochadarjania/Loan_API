using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loan_API.Infrastructure.Migrations
{
    public partial class CreateLoanAPIDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanPeriodTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    LoanTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoanStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_LoanStatuses_LoanStatusId",
                        column: x => x.LoanStatusId,
                        principalTable: "LoanStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_LoanTypes_LoanTypeId",
                        column: x => x.LoanTypeId,
                        principalTable: "LoanTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyName" },
                values: new object[,]
                {
                    { 1, "GEL" },
                    { 2, "USD" },
                    { 3, "EUR" }
                });

            migrationBuilder.InsertData(
                table: "LoanStatuses",
                columns: new[] { "Id", "LoanStatusName" },
                values: new object[,]
                {
                    { 1, "In process" },
                    { 2, "Approved" },
                    { 3, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "LoanTypes",
                columns: new[] { "Id", "LoanTypeName" },
                values: new object[,]
                {
                    { 1, "Quick loan" },
                    { 2, "Auto loan" },
                    { 3, "Installment" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Accountant" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "FirstName", "IsBlocked", "LastName", "Password", "RoleId", "Salary", "UserName" },
                values: new object[,]
                {
                    { 1, 20, "Thomas", false, "Hardy", "$2a$11$MHcnoX7AIQACV.gFMpgsUOxYfEXuvvROoDZFm3SopnXvtjbI2fqka", 1, 1000.0, "admin@gmail.com" },
                    { 2, 25, "Christina", true, "Berglund", "$2a$11$cNDX06Wdi0FgtlI1JROfB.q8zlybBbKytLmeTisvUDveTRfKdxTxW", 2, 1000.0, "user2@gmail.com" },
                    { 3, 30, "Ana", false, "Trujillo", "$2a$11$UXknr4GH9Wrq7Z6NZTXSA.CeVfbUybopeRHwftjcS6S9D8PXMR06C", 2, 1000.0, "user3@gmail.com" },
                    { 4, 35, "Maria", false, "Anders", "$2a$11$J4ULLMQ0RiRYIMtdtGq6WOe2wqT10pZKIdg26A8uBlg1dAmbd7S1y", 2, 1000.0, "user4@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "Amount", "CurrencyId", "LoanPeriodTime", "LoanStatusId", "LoanTypeId", "UserId" },
                values: new object[] { 1, 2000.0, 1, new DateTime(2023, 12, 31, 5, 10, 20, 0, DateTimeKind.Unspecified), 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "Amount", "CurrencyId", "LoanPeriodTime", "LoanStatusId", "LoanTypeId", "UserId" },
                values: new object[] { 2, 3000.0, 2, new DateTime(2023, 12, 31, 5, 10, 20, 0, DateTimeKind.Unspecified), 2, 2, 3 });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "Amount", "CurrencyId", "LoanPeriodTime", "LoanStatusId", "LoanTypeId", "UserId" },
                values: new object[] { 3, 4000.0, 3, new DateTime(2023, 12, 31, 5, 10, 20, 0, DateTimeKind.Unspecified), 3, 3, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CurrencyId",
                table: "Loans",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanStatusId",
                table: "Loans",
                column: "LoanStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanTypeId",
                table: "Loans",
                column: "LoanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "LoanStatuses");

            migrationBuilder.DropTable(
                name: "LoanTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
