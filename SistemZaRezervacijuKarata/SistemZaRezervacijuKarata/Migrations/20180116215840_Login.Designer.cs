﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SistemZaRezervacijuKarata.Models;
using System;

namespace SistemZaRezervacijuKarata.Migrations
{
    [DbContext(typeof(SistemZaRezervacijuKarataContext))]
    [Migration("20180116215840_Login")]
    partial class Login
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SistemZaRezervacijuKarata.Models.Film", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Drzava")
                        .IsRequired();

                    b.Property<string>("DuzinaTranjanja")
                        .IsRequired();

                    b.Property<string>("Godina")
                        .IsRequired();

                    b.Property<string>("Naslov")
                        .IsRequired();

                    b.Property<string>("Opis")
                        .IsRequired();

                    b.Property<string>("OrgininalniNaslov")
                        .IsRequired();

                    b.Property<DateTime>("PocetakPrikazivanja");

                    b.Property<string>("SlikaUrl")
                        .IsRequired();

                    b.Property<string>("YoutubeUrl")
                        .IsRequired();

                    b.Property<string>("Zanr")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("SistemZaRezervacijuKarata.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<string>("Email");

                    b.Property<string>("Ime");

                    b.Property<string>("Password");

                    b.Property<string>("Pol");

                    b.Property<string>("Prezime");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("SistemZaRezervacijuKarata.Models.Projekcija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<int>("FilmId");

                    b.Property<int>("SalaId");

                    b.Property<int>("SlobodnoSedista");

                    b.Property<DateTime>("Vreme");

                    b.HasKey("ID");

                    b.HasIndex("FilmId");

                    b.HasIndex("SalaId");

                    b.ToTable("Projekcija");
                });

            modelBuilder.Entity("SistemZaRezervacijuKarata.Models.Rezervacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojKarata");

                    b.Property<int>("KorisnikId");

                    b.Property<int>("ProjekcijaId");

                    b.HasKey("ID");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("ProjekcijaId");

                    b.ToTable("Rezervacija");
                });

            modelBuilder.Entity("SistemZaRezervacijuKarata.Models.Sala", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojSedista");

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.Property<string>("Tehnologija")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("SistemZaRezervacijuKarata.Models.Projekcija", b =>
                {
                    b.HasOne("SistemZaRezervacijuKarata.Models.Film", "Film")
                        .WithMany("Projekcije")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemZaRezervacijuKarata.Models.Sala", "Sala")
                        .WithMany()
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemZaRezervacijuKarata.Models.Rezervacija", b =>
                {
                    b.HasOne("SistemZaRezervacijuKarata.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemZaRezervacijuKarata.Models.Projekcija", "Projekcija")
                        .WithMany()
                        .HasForeignKey("ProjekcijaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
