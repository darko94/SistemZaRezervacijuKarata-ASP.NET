using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemZaRezervacijuKarata.Migrations
{
    public partial class RezervacijaPromenjenKorisnikNaAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Korisnik_KorisnikId",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_KorisnikId",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Rezervacija");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Rezervacija",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_ApplicationUserId",
                table: "Rezervacija",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_AspNetUsers_ApplicationUserId",
                table: "Rezervacija",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_AspNetUsers_ApplicationUserId",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_ApplicationUserId",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Rezervacija");

            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Rezervacija",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KorisnikId",
                table: "Rezervacija",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Korisnik_KorisnikId",
                table: "Rezervacija",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
