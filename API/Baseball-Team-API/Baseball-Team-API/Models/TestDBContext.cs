using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Baseball_Team_API.Models
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendee> Attendee { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Member> Member { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server = tcp:diptestserver.database.windows.net, 1433; Initial Catalog = TestDB; Persist Security Info = False; User ID = Bradley; Password = R@ptorskills; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.Username });

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Paid).HasColumnType("money");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Attendee)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventID");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Attendee)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Forfeit).HasColumnName("forfeit");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temporal).HasColumnType("datetime");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.AuthLevel)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.Salt).IsRequired();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
