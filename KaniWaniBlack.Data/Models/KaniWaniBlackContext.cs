using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KaniWaniBlack.Data.Models
{
    public partial class KaniWaniBlackContext : DbContext
    {
        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserVocab> UserVocab { get; set; }
        public virtual DbSet<WaniKaniUser> WaniKaniUser { get; set; }
        public virtual DbSet<WaniKaniVocab> WaniKaniVocab { get; set; }

        public KaniWaniBlackContext(DbContextOptions<KaniWaniBlackContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLogs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__User__536C85E4C904BC2C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LastApplicationUsed)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginAttempt).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserVocab>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserSynonyms).IsUnicode(false);

                entity.Property(e => e.WkvocabId).HasColumnName("WKVocabID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserVocab)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserVocab__UserI__3A4CA8FD");

                entity.HasOne(d => d.Wkvocab)
                    .WithMany(p => p.UserVocab)
                    .HasForeignKey(d => d.WkvocabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserVocab__WKVoc__3B40CD36");
            });

            modelBuilder.Entity<WaniKaniUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Gravatar)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WkApiKey)
                    .HasColumnName("WKApiKey")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Wklevel).HasColumnName("WKLevel");

                entity.Property(e => e.Wkusername)
                    .HasColumnName("WKUsername")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WaniKaniUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WaniKaniU__UserI__3587F3E0");
            });

            modelBuilder.Entity<WaniKaniVocab>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Character).IsRequired();

                entity.Property(e => e.Kana).IsRequired();

                entity.Property(e => e.Meaning)
                    .IsRequired()
                    .IsUnicode(false);
            });
        }
    }
}