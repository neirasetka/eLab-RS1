using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eLab.Data.Migrations
{
    public partial class addedAnalizaNalazPacijentTipUzorkaUzorkovanjeUzorkovanjeMaterijali : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "ReferentneVrijednosti",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "AnalizaId",
                table: "Parametri",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RhFactor",
                table: "KrvnaGrupa",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "KrvnaGrupaId",
                table: "Karton",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Analiza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analiza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacijent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KartonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacijent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacijent_Karton_KartonId",
                        column: x => x.KartonId,
                        principalTable: "Karton",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipUzorka",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipUzorka", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzorkovanje",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzorkovanje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nalaz",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacijentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalizaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nalaz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nalaz_Analiza_AnalizaId",
                        column: x => x.AnalizaId,
                        principalTable: "Analiza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nalaz_Pacijent_PacijentId",
                        column: x => x.PacijentId,
                        principalTable: "Pacijent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UzorkovanjeMaterijali",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UzorkovanjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterijaliId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzorkovanjeMaterijali", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UzorkovanjeMaterijali_Materijali_MaterijaliId",
                        column: x => x.MaterijaliId,
                        principalTable: "Materijali",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UzorkovanjeMaterijali_Uzorkovanje_UzorkovanjeId",
                        column: x => x.UzorkovanjeId,
                        principalTable: "Uzorkovanje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parametri_AnalizaId",
                table: "Parametri",
                column: "AnalizaId");

            migrationBuilder.CreateIndex(
                name: "IX_Karton_KrvnaGrupaId",
                table: "Karton",
                column: "KrvnaGrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_Nalaz_AnalizaId",
                table: "Nalaz",
                column: "AnalizaId");

            migrationBuilder.CreateIndex(
                name: "IX_Nalaz_PacijentId",
                table: "Nalaz",
                column: "PacijentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacijent_KartonId",
                table: "Pacijent",
                column: "KartonId");

            migrationBuilder.CreateIndex(
                name: "IX_UzorkovanjeMaterijali_MaterijaliId",
                table: "UzorkovanjeMaterijali",
                column: "MaterijaliId");

            migrationBuilder.CreateIndex(
                name: "IX_UzorkovanjeMaterijali_UzorkovanjeId",
                table: "UzorkovanjeMaterijali",
                column: "UzorkovanjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Karton_KrvnaGrupa_KrvnaGrupaId",
                table: "Karton",
                column: "KrvnaGrupaId",
                principalTable: "KrvnaGrupa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parametri_Analiza_AnalizaId",
                table: "Parametri",
                column: "AnalizaId",
                principalTable: "Analiza",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karton_KrvnaGrupa_KrvnaGrupaId",
                table: "Karton");

            migrationBuilder.DropForeignKey(
                name: "FK_Parametri_Analiza_AnalizaId",
                table: "Parametri");

            migrationBuilder.DropTable(
                name: "Nalaz");

            migrationBuilder.DropTable(
                name: "TipUzorka");

            migrationBuilder.DropTable(
                name: "UzorkovanjeMaterijali");

            migrationBuilder.DropTable(
                name: "Analiza");

            migrationBuilder.DropTable(
                name: "Pacijent");

            migrationBuilder.DropTable(
                name: "Uzorkovanje");

            migrationBuilder.DropIndex(
                name: "IX_Parametri_AnalizaId",
                table: "Parametri");

            migrationBuilder.DropIndex(
                name: "IX_Karton_KrvnaGrupaId",
                table: "Karton");

            migrationBuilder.DropColumn(
                name: "AnalizaId",
                table: "Parametri");

            migrationBuilder.DropColumn(
                name: "KrvnaGrupaId",
                table: "Karton");

            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "ReferentneVrijednosti",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RhFactor",
                table: "KrvnaGrupa",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");
        }
    }
}
