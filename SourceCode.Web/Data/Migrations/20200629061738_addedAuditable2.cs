using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SourceCode.Web.Data.Migrations
{
    public partial class addedAuditable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "ClientProject",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ClientProject",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Name",
                table: "Clients",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Name",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "ClientProject");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ClientProject");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
