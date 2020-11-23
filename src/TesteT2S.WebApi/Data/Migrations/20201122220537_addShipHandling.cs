using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteT2S.WebApi.Data.Migrations
{
    public partial class addShipHandling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handlings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerId = table.Column<int>(type: "int", nullable: false),
                    Ship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HandlingType = table.Column<byte>(type: "tinyint", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handlings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handlings_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Handlings_ContainerId",
                table: "Handlings",
                column: "ContainerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handlings");
        }
    }
}
