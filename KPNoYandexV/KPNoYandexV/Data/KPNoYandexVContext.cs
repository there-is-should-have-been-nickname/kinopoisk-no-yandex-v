using System;
using KPNoYandexV.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KPNoYandexV.Data
{
    public partial class KPNoYandexVContext : DbContext
    {
        public KPNoYandexVContext()
        {
        }

        public KPNoYandexVContext(DbContextOptions<KPNoYandexVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmsActor> FilmsActors { get; set; }
        public virtual DbSet<FilmsGenre> FilmsGenres { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=KPNoYandexV;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.DateBirth).HasColumnType("datetime");

                entity.Property(e => e.FacePath).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasMaxLength(600)
                    .HasColumnName("Description_");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("Name_");

                entity.Property(e => e.PosterPath).HasMaxLength(50);

                entity.Property(e => e.Year)
                    .HasColumnType("datetime")
                    .HasColumnName("Year_");
            });

            modelBuilder.Entity<FilmsActor>(entity =>
            {
                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.FilmsActors)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FilmsActo__Actor__4222D4EF");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmsActors)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FilmsActo__FilmI__412EB0B6");
            });

            modelBuilder.Entity<FilmsGenre>(entity =>
            {
                entity.HasOne(d => d.Film)
                    .WithMany(p => p.FilmsGenres)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FilmsGenr__FilmI__3D5E1FD2");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.FilmsGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FilmsGenr__Genre__3E52440B");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("Name_");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
