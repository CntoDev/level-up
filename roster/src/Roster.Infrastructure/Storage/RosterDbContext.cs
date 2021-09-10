using Microsoft.EntityFrameworkCore;
using Roster.Core.Domain;

namespace Roster.Infrastructure.Storage
{
    public class RosterDbContext : DbContext
    {
        public RosterDbContext(DbContextOptions<RosterDbContext> options) : base(options)
        {
            
        }

        public DbSet<ApplicationForm> ApplicationForms { get; set; }

        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationForm>()
                        .HasKey(af => af.Nickname);
            
            modelBuilder.Entity<ApplicationForm>()
                        .OwnsMany<Arma3Dlc>(af => af.OwnedDlcs);

            modelBuilder.Entity<Member>()
                        .HasKey(m => m.Nickname);

            modelBuilder.Entity<Member>()
                        .HasIndex(m => m.Email);

            modelBuilder.Entity<Member>()
                        .Property(m => m.Email)
                        .IsRequired();

            modelBuilder.Entity<Member>()
                        .Property("_verificationCode")
                        .HasColumnName("VerificationCode");

            modelBuilder.Entity<Member>()
                        .Property("_verificationTime")
                        .HasColumnName("VerificationTime");
            
            modelBuilder.Entity<Member>()
                        .Property("_emailVerified")
                        .HasColumnName("EmailVerified");
        }
    }
}