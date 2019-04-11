using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank_Accounts.Migrations
{
    public partial class FirMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForLandRes",
                columns: table => new
                {
                    ForLandRId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForLandRes", x => x.ForLandRId);
                });

            migrationBuilder.CreateTable(
                name: "Transactiones",
                columns: table => new
                {
                    TansactionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    ForLandRId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactiones", x => x.TansactionId);
                    table.ForeignKey(
                        name: "FK_Transactiones_ForLandRes_ForLandRId",
                        column: x => x.ForLandRId,
                        principalTable: "ForLandRes",
                        principalColumn: "ForLandRId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactiones_ForLandRId",
                table: "Transactiones",
                column: "ForLandRId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactiones");

            migrationBuilder.DropTable(
                name: "ForLandRes");
        }
    }
}
