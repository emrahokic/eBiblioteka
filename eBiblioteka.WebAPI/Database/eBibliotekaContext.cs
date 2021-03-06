﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eBiblioteka.WebAPI.Database
{
    public partial class eBibliotekaContext : DbContext
    {
        public eBibliotekaContext()
        {
        }

        public eBibliotekaContext(DbContextOptions<eBibliotekaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Biblioteka> Biblioteka { get; set; }
        public virtual DbSet<Clan> Clan { get; set; }
        public virtual DbSet<Clanarina> Clanarina { get; set; }
        public virtual DbSet<Drzava> Drzava { get; set; }
        public virtual DbSet<Grad> Grad { get; set; }
        public virtual DbSet<Izdavac> Izdavac { get; set; }
        public virtual DbSet<Jezik> Jezik { get; set; }
        public virtual DbSet<Knjiga> Knjiga { get; set; }
        public virtual DbSet<KnjigaIzdavanje> KnjigaIzdavanje { get; set; }
        public virtual DbSet<KnjigaPisac> KnjigaPisac { get; set; }
        public virtual DbSet<KnjigaRezervacija> KnjigaRezervacija { get; set; }
        public virtual DbSet<KnjigaZanr> KnjigaZanr { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<KorisnikRola> KorisnikRola { get; set; }
        public virtual DbSet<Notifikacija> Notifikacija { get; set; }
        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Pisac> Pisac { get; set; }
        public virtual DbSet<Rola> Rola { get; set; }
        public virtual DbSet<Tip> Tip { get; set; }
        public virtual DbSet<Uposlenik> Uposlenik { get; set; }
        public virtual DbSet<Zanr> Zanr { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Biblioteka>(entity =>
            {
                entity.Property(e => e.BibliotekaId).HasColumnName("BibliotekaID");

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.BrTelefon).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.GradId).HasColumnName("GradID");

                entity.Property(e => e.LatLong).HasMaxLength(50);

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.TipId).HasColumnName("TipID");

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Biblioteka)
                    .HasForeignKey(d => d.GradId)
                    .HasConstraintName("FK__Bibliotek__GradI__5BE2A6F2");

                entity.HasOne(d => d.Tip)
                    .WithMany(p => p.Biblioteka)
                    .HasForeignKey(d => d.TipId)
                    .HasConstraintName("FK_Biblioteka_Tip");
            });

            modelBuilder.Entity<Clan>(entity =>
            {
                entity.Property(e => e.ClanId).HasColumnName("ClanID");

                entity.Property(e => e.BibliotekaId).HasColumnName("BibliotekaID");

                entity.Property(e => e.ClanskiBroj)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DatumUclanjivanja).HasColumnType("datetime");

                entity.Property(e => e.OsobaId).HasColumnName("OsobaID");

                entity.HasOne(d => d.Biblioteka)
                    .WithMany(p => p.Clan)
                    .HasForeignKey(d => d.BibliotekaId)
                    .HasConstraintName("FK__Clan__Biblioteka__5DCAEF64");

                entity.HasOne(d => d.Osoba)
                    .WithMany(p => p.Clan)
                    .HasForeignKey(d => d.OsobaId)
                    .HasConstraintName("FK__Clan__OsobaID__5EBF139D");
            });

            modelBuilder.Entity<Clanarina>(entity =>
            {
                entity.Property(e => e.ClanarinaId).HasColumnName("ClanarinaID");

                entity.Property(e => e.BibliotekaId).HasColumnName("BibliotekaID");

                entity.Property(e => e.ClanId).HasColumnName("ClanID");

                entity.Property(e => e.DatumClanarine).HasColumnType("datetime");

                entity.Property(e => e.DatumIsteka).HasColumnType("datetime");

                entity.Property(e => e.Iznos).HasColumnType("money");

                entity.HasOne(d => d.Biblioteka)
                    .WithMany(p => p.Clanarina)
                    .HasForeignKey(d => d.BibliotekaId)
                    .HasConstraintName("FK__Clanarina__Bibli__5FB337D6");

                entity.HasOne(d => d.Clan)
                    .WithMany(p => p.Clanarina)
                    .HasForeignKey(d => d.ClanId)
                    .HasConstraintName("FK__Clanarina__ClanI__60A75C0F");
            });

            modelBuilder.Entity<Drzava>(entity =>
            {
                entity.Property(e => e.DrzavaId).HasColumnName("DrzavaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Skracenica).HasMaxLength(15);
            });

            modelBuilder.Entity<Grad>(entity =>
            {
                entity.Property(e => e.GradId).HasColumnName("GradID");

                entity.Property(e => e.DrzavaId).HasColumnName("DrzavaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Drzava)
                    .WithMany(p => p.Grad)
                    .HasForeignKey(d => d.DrzavaId)
                    .HasConstraintName("FK__Grad__DrzavaID__619B8048");
            });

            modelBuilder.Entity<Izdavac>(entity =>
            {
                entity.Property(e => e.IzdavacId).HasColumnName("IzdavacID");

                entity.Property(e => e.GradId).HasColumnName("GradID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Izdavac)
                    .HasForeignKey(d => d.GradId)
                    .HasConstraintName("FK__Izdavac__GradID__628FA481");
            });

            modelBuilder.Entity<Jezik>(entity =>
            {
                entity.Property(e => e.JezikId).HasColumnName("JezikID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Knjiga>(entity =>
            {
                entity.Property(e => e.KnjigaId).HasColumnName("KnjigaID");

                entity.Property(e => e.BibliotekaId).HasColumnName("BibliotekaID");

                entity.Property(e => e.Godina).HasColumnType("date");

                entity.Property(e => e.InventarniBroj).IsRequired();

                entity.Property(e => e.IzdavacId).HasColumnName("IzdavacID");

                entity.Property(e => e.JazikId).HasColumnName("JazikID");

                entity.Property(e => e.Naziv).HasMaxLength(220);

                entity.Property(e => e.Signatura).IsRequired();

                entity.HasOne(d => d.Biblioteka)
                    .WithMany(p => p.Knjiga)
                    .HasForeignKey(d => d.BibliotekaId)
                    .HasConstraintName("FK__Knjiga__Bibliote__6383C8BA");

                entity.HasOne(d => d.Izdavac)
                    .WithMany(p => p.Knjiga)
                    .HasForeignKey(d => d.IzdavacId)
                    .HasConstraintName("FK__Knjiga__IzdavacI__6477ECF3");

                entity.HasOne(d => d.Jazik)
                    .WithMany(p => p.Knjiga)
                    .HasForeignKey(d => d.JazikId)
                    .HasConstraintName("FK__Knjiga__JazikID__656C112C");
            });

            modelBuilder.Entity<KnjigaIzdavanje>(entity =>
            {
                entity.HasKey(e => e.KnjigaIzdavanje1);

                entity.Property(e => e.KnjigaIzdavanje1).HasColumnName("KnjigaIzdavanje");

                entity.Property(e => e.ClanId).HasColumnName("ClanID");

                entity.Property(e => e.DatumPovratka).HasColumnType("datetime");

                entity.Property(e => e.DatumPreuzimanja).HasColumnType("datetime");

                entity.Property(e => e.KnjigaId).HasColumnName("KnjigaID");

                entity.HasOne(d => d.Clan)
                    .WithMany(p => p.KnjigaIzdavanje)
                    .HasForeignKey(d => d.ClanId)
                    .HasConstraintName("FK__KnjigaIzd__ClanI__66603565");

                entity.HasOne(d => d.Knjiga)
                    .WithMany(p => p.KnjigaIzdavanje)
                    .HasForeignKey(d => d.KnjigaId)
                    .HasConstraintName("FK_KnjigaIzdavanje_Knjiga");
            });

            modelBuilder.Entity<KnjigaPisac>(entity =>
            {
                entity.HasKey(e => new { e.KnjigaId, e.PisacId });

                entity.Property(e => e.KnjigaId).HasColumnName("KnjigaID");

                entity.Property(e => e.PisacId).HasColumnName("PisacID");

                entity.HasOne(d => d.Knjiga)
                    .WithMany(p => p.KnjigaPisac)
                    .HasForeignKey(d => d.KnjigaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnjigaPisac_Knjiga");

                entity.HasOne(d => d.Pisac)
                    .WithMany(p => p.KnjigaPisac)
                    .HasForeignKey(d => d.PisacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnjigaPisac_Pisac");
            });

            modelBuilder.Entity<KnjigaRezervacija>(entity =>
            {
                entity.HasKey(e => e.KnjigaRezervacija1);

                entity.Property(e => e.KnjigaRezervacija1).HasColumnName("KnjigaRezervacija");

                entity.Property(e => e.ClanId).HasColumnName("ClanID");

                entity.Property(e => e.DatumRezervacije).HasColumnType("datetime");

                entity.Property(e => e.KnjigaId).HasColumnName("KnjigaID");

                entity.HasOne(d => d.Clan)
                    .WithMany(p => p.KnjigaRezervacija)
                    .HasForeignKey(d => d.ClanId)
                    .HasConstraintName("FK__KnjigaRez__ClanI__6A30C649");

                entity.HasOne(d => d.Knjiga)
                    .WithMany(p => p.KnjigaRezervacija)
                    .HasForeignKey(d => d.KnjigaId)
                    .HasConstraintName("FK_KnjigaRezervacija_Knjiga");
            });

            modelBuilder.Entity<KnjigaZanr>(entity =>
            {
                entity.HasKey(e => new { e.KnjigaId, e.ZanrId });

                entity.Property(e => e.KnjigaId).HasColumnName("KnjigaID");

                entity.Property(e => e.ZanrId).HasColumnName("ZanrID");

                entity.HasOne(d => d.Knjiga)
                    .WithMany(p => p.KnjigaZanr)
                    .HasForeignKey(d => d.KnjigaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnjigaZanr_Knjiga");

                entity.HasOne(d => d.Zanr)
                    .WithMany(p => p.KnjigaZanr)
                    .HasForeignKey(d => d.ZanrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KnjigaZan__ZanrI__6C190EBB");
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.KorisnickaSifraHash)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.KorisnickoIme)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastSalt).HasColumnType("datetime");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Slika).HasMaxLength(150);
            });

            modelBuilder.Entity<KorisnikRola>(entity =>
            {
                entity.HasKey(e => new { e.RolaId, e.KorisnikId });

                entity.Property(e => e.RolaId).HasColumnName("RolaID");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.DatumIzmjene).HasColumnType("datetime");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.KorisnikRola)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KorisnikR__Koris__6EF57B66");

                entity.HasOne(d => d.Rola)
                    .WithMany(p => p.KorisnikRola)
                    .HasForeignKey(d => d.RolaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KorisnikR__RolaI__6FE99F9F");
            });

            modelBuilder.Entity<Notifikacija>(entity =>
            {
                entity.Property(e => e.NotifikacijaId).HasColumnName("NotifikacijaID");

                entity.Property(e => e.BibliotekaId).HasColumnName("BibliotekaID");

                entity.Property(e => e.ClanId).HasColumnName("ClanID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.Opis).HasMaxLength(100);

                entity.HasOne(d => d.Biblioteka)
                    .WithMany(p => p.Notifikacija)
                    .HasForeignKey(d => d.BibliotekaId)
                    .HasConstraintName("FK_Notifikacija_Biblioteka");

                entity.HasOne(d => d.Clan)
                    .WithMany(p => p.Notifikacija)
                    .HasForeignKey(d => d.ClanId)
                    .HasConstraintName("FK_Notifikacija_Clan");
            });

            modelBuilder.Entity<Osoba>(entity =>
            {
                entity.Property(e => e.OsobaId).HasColumnName("OsobaID");

                entity.Property(e => e.DatumRodjenja).HasMaxLength(50);

                entity.Property(e => e.GradId).HasColumnName("GradID");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Jmbg)
                    .HasColumnName("JMBG")
                    .HasMaxLength(20);

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Osoba)
                    .HasForeignKey(d => d.GradId)
                    .HasConstraintName("FK__Osoba__GradID__70DDC3D8");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Osoba)
                    .HasForeignKey(d => d.KorisnikId)
                    .HasConstraintName("FK_Osoba_Korisnik");
            });

            modelBuilder.Entity<Pisac>(entity =>
            {
                entity.Property(e => e.PisacId).HasColumnName("PisacID");

                entity.Property(e => e.Biografija).HasMaxLength(1000);

                entity.Property(e => e.GodinaRodjenja).HasColumnType("datetime");

                entity.Property(e => e.GodinaSmrti).HasColumnType("datetime");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Slika).HasMaxLength(150);
            });

            modelBuilder.Entity<Rola>(entity =>
            {
                entity.Property(e => e.RolaId).HasColumnName("RolaID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.Property(e => e.TipId).HasColumnName("TipID");

                entity.Property(e => e.Icona).HasMaxLength(50);

                entity.Property(e => e.Naziv).HasMaxLength(50);
            });

            modelBuilder.Entity<Uposlenik>(entity =>
            {
                entity.Property(e => e.UposlenikId).HasColumnName("UposlenikID");

                entity.Property(e => e.BibliotekaId).HasColumnName("BibliotekaID");

                entity.Property(e => e.BrojRadnika)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DatumZaposlenja).HasColumnType("datetime");

                entity.Property(e => e.OsobaId).HasColumnName("OsobaID");

                entity.HasOne(d => d.Biblioteka)
                    .WithMany(p => p.Uposlenik)
                    .HasForeignKey(d => d.BibliotekaId)
                    .HasConstraintName("FK__Uposlenik__Bibli__71D1E811");

                entity.HasOne(d => d.Osoba)
                    .WithMany(p => p.Uposlenik)
                    .HasForeignKey(d => d.OsobaId)
                    .HasConstraintName("FK__Uposlenik__Osoba__72C60C4A");
            });

            modelBuilder.Entity<Zanr>(entity =>
            {
                entity.Property(e => e.ZanrId).HasColumnName("ZanrID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
