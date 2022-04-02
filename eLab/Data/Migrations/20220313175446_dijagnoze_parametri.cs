using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eLab.Data.Migrations
{
    public partial class dijagnoze_parametri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dijagnoza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatinskiNaziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LokalniNaziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifraMKB10 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dijagnoza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kratica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MikrobioloskiUzorak = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametri", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dijagnoza");

            migrationBuilder.DropTable(
                name: "Parametri");
        }
    }
}
