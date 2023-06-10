using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesProject.Persistance.Migrations
{
    public partial class AllowNullsForProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b35e6e2-b5c0-4411-bddf-c4b2dc943c8f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f24e021-4af2-49e8-861f-c149a2eedf3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7dfd0e7-bac5-4d26-82e6-3e5602026d7b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2033441e-ade3-418f-9ad1-72910e8d5790", "10", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "883ed01f-c0f0-4d37-98b5-d0b22759ef14", "10", "manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cfbd0f05-5f19-4218-85ca-5824876e3964", "10", "employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2033441e-ade3-418f-9ad1-72910e8d5790");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "883ed01f-c0f0-4d37-98b5-d0b22759ef14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfbd0f05-5f19-4218-85ca-5824876e3964");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b35e6e2-b5c0-4411-bddf-c4b2dc943c8f", "10", "employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f24e021-4af2-49e8-861f-c149a2eedf3e", "10", "manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d7dfd0e7-bac5-4d26-82e6-3e5602026d7b", "10", "admin", "ADMIN" });
        }
    }
}
