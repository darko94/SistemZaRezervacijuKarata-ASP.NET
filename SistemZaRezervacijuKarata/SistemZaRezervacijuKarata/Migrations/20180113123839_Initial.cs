using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemZaRezervacijuKarata.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Drzava = table.Column<string>(nullable: true),
                    DuzinaTranjanja = table.Column<string>(nullable: true),
                    Godina = table.Column<string>(nullable: true),
                    Naslov = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    OrgininalniNaslov = table.Column<string>(nullable: true),
                    PocetakPrikazivanja = table.Column<DateTime>(nullable: false),
                    SlikaUrl = table.Column<string>(nullable: true),
                    YoutubeUrl = table.Column<string>(nullable: true),
                    Zanr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Film");
        }
    }
}
