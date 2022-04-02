using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eLab.Data.Migrations
{
    public partial class addedUputnicaDijagnozaUputnicaParametriUputnica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uputnica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipUzorkaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UzorkovanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacijentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uputnica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uputnica_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uputnica_Pacijent_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uputnica_TipUzorka_TipUzorkaId",
                        column: x => x.TipUzorkaId,
                        principalTable: "TipUzorka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uputnica_Uzorkovanje_UzorkovanjeId",
                        column: x => x.UzorkovanjeId,
                        principalTable: "Uzorkovanje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DijagnozaUputnica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DijagnozaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UputnicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DijagnozaUputnica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DijagnozaUputnica_Dijagnoza_DijagnozaId",
                        column: x => x.DijagnozaId,
                        principalTable: "Dijagnoza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DijagnozaUputnica_Uputnica_UputnicaId",
                        column: x => x.UputnicaId,
                        principalTable: "Uputnica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParametriUputnica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParametriId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UputnicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametriUputnica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParametriUputnica_Parametri_ParametriId",
                        column: x => x.ParametriId,
                        principalTable: "Parametri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParametriUputnica_Uputnica_UputnicaId",
                        column: x => x.UputnicaId,
                        principalTable: "Uputnica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DijagnozaUputnica_DijagnozaId",
                table: "DijagnozaUputnica",
                column: "DijagnozaId");

            migrationBuilder.CreateIndex(
                name: "IX_DijagnozaUputnica_UputnicaId",
                table: "DijagnozaUputnica",
                column: "UputnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParametriUputnica_ParametriId",
                table: "ParametriUputnica",
                column: "ParametriId");

            migrationBuilder.CreateIndex(
                name: "IX_ParametriUputnica_UputnicaId",
                table: "ParametriUputnica",
                column: "UputnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Uputnica_KorisnikId",
                table: "Uputnica",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Uputnica_PacijentId",
                table: "Uputnica",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Uputnica_TipUzorkaId",
                table: "Uputnica",
                column: "TipUzorkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Uputnica_UzorkovanjeId",
                table: "Uputnica",
                column: "UzorkovanjeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DijagnozaUputnica");

            migrationBuilder.DropTable(
                name: "ParametriUputnica");

            migrationBuilder.DropTable(
                name: "Uputnica");
        }
    }
}
