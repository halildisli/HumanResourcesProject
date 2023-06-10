using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesProject.Persistance.Migrations
{
    public partial class PermissionAndExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "206fdfed-0f4f-4785-9731-7f141747e2c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ee72b38-9db6-484e-81f1-8b239a40ba80");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8fd2302-3b78-4271-a49c-de0bb8cb2f4c");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountOfExpense = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfExpense = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseType = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    AdvanceStatus = table.Column<int>(type: "int", nullable: false),
                    DateOfResponse = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionType = table.Column<int>(type: "int", nullable: false),
                    StartOfPermissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOfPermissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CountOfPermittedDays = table.Column<int>(type: "int", nullable: true),
                    ApprovalState = table.Column<bool>(type: "bit", nullable: true),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Permissions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ad03c9b-cc4d-4d83-ba2e-8412e806c7df", "10", "manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d49faf65-6d0b-4a9f-89b6-db43657bb17c", "10", "employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f1ea6eb0-1704-4041-9fc7-87104edf5bb4", "10", "admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmployeeId",
                table: "Expenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PersonId",
                table: "Expenses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_EmployeeId",
                table: "Permissions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PersonId",
                table: "Permissions",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ad03c9b-cc4d-4d83-ba2e-8412e806c7df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d49faf65-6d0b-4a9f-89b6-db43657bb17c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1ea6eb0-1704-4041-9fc7-87104edf5bb4");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "206fdfed-0f4f-4785-9731-7f141747e2c5", "10", "employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ee72b38-9db6-484e-81f1-8b239a40ba80", "10", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8fd2302-3b78-4271-a49c-de0bb8cb2f4c", "10", "manager", "MANAGER" });
        }
    }
}
