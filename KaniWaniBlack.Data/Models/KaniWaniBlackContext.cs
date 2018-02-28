using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KaniWaniBlack.Data.Models
{
    public partial class KaniWaniBlackContext : DbContext
    {
        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserVocab> UserVocab { get; set; }
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
                    .HasName("UQ__Users__536C85E4EBD413B7")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApiKey)
                    .HasMaxLength(100)
                    .IsUnicode(false);

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
                    .HasConstraintName("FK__UserVocab__UserI__18EBB532");

                entity.HasOne(d => d.Wkvocab)
                    .WithMany(p => p.UserVocab)
                    .HasForeignKey(d => d.WkvocabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserVocab__WKVoc__19DFD96B");
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