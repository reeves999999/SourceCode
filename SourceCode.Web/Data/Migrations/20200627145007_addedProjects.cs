using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SourceCode.Web.Data.Migrations
{
    public partial class addedProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProject_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientProject_ClientId",
                table: "ClientProject",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientProject");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Clients");
        }
    }
}
