using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesProject.Persistance.Migrations
{
    public partial class AddPersonnelClassAndController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Companies_CompanyId",
                table: "Personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Departments_DepartmentId",
                table: "Personnels");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Jobs_JobId",
                table: "Personnels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personnels",
                table: "Personnels");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3617f3ab-d697-43dd-8e3a-ea51740ee537");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c48b332a-88f2-4285-964e-5cff1f5e08e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efd83ad3-9e11-46c7-aff1-b2dab497bc79");

            migrationBuilder.RenameTable(
                name: "Personnels",
                newName: "Personnel");

            migrationBuilder.RenameIndex(
                name: "IX_Personnels_JobId",
                table: "Personnel",
                newName: "IX_Personnel_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Personnels_DepartmentId",
                table: "Personnel",
                newName: "IX_Personnel_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Personnels_CompanyId",
                table: "Personnel",
                newName: "IX_Personnel_CompanyId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartingDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartingDate",
                table: "Personnel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondSurname",
                table: "Personnel",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "SecondName",
                table: "Personnel",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Personnel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlaceOfBirth",
                table: "Personnel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Personnel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "Personnel",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Personnel",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Personnel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Personnel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Personnel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personnel",
                table: "Personnel",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1d95e64-0dac-4f40-bf0f-18cd9498b0ec", "10", "manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbdca0ca-bcb8-4004-bc72-0e016b3ec7b2", "10", "employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd50cf4c-348a-4b3e-97c9-727ac79b31fd", "10", "admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Companies_CompanyId",
                table: "Personnel",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Departments_DepartmentId",
                table: "Personnel",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Jobs_JobId",
                table: "Personnel",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Companies_CompanyId",
                table: "Personnel");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Departments_DepartmentId",
                table: "Personnel");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Jobs_JobId",
                table: "Personnel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personnel",
                table: "Personnel");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1d95e64-0dac-4f40-bf0f-18cd9498b0ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbdca0ca-bcb8-4004-bc72-0e016b3ec7b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd50cf4c-348a-4b3e-97c9-727ac79b31fd");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Personnel");

            migrationBuilder.RenameTable(
                name: "Personnel",
                newName: "Personnels");

            migrationBuilder.RenameIndex(
                name: "IX_Personnel_JobId",
                table: "Personnels",
                newName: "IX_Personnels_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Personnel_DepartmentId",
                table: "Personnels",
                newName: "IX_Personnels_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Personnel_CompanyId",
                table: "Personnels",
                newName: "IX_Personnels_CompanyId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartingDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartingDate",
                table: "Personnels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "SecondSurname",
                table: "Personnels",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondName",
                table: "Personnels",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Personnels",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceOfBirth",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Personnels",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Personnels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Personnels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personnels",
                table: "Personnels",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3617f3ab-d697-43dd-8e3a-ea51740ee537", "10", "manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c48b332a-88f2-4285-964e-5cff1f5e08e6", "10", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "efd83ad3-9e11-46c7-aff1-b2dab497bc79", "10", "employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Companies_CompanyId",
                table: "Personnels",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Departments_DepartmentId",
                table: "Personnels",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Jobs_JobId",
                table: "Personnels",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
