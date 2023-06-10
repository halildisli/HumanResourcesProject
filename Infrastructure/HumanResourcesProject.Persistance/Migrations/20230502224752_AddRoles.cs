using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesProject.Persistance.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
