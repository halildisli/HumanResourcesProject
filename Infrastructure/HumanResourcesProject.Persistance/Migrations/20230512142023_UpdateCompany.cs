using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourcesProject.Persistance.Migrations
{
    public partial class UpdateCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractStartDate",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MersisNo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmployees",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAdministration",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxNo",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContractStartDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "MersisNo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployees",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TaxAdministration",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TaxNo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

           
        }
    }
}
