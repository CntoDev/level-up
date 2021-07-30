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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationForm>()
                        .HasKey(af => af.Nickname);
            
            modelBuilder.Entity<ApplicationForm>()
                        .OwnsMany<Arma3Dlc>(af => af.OwnedDlcs);
        }
    }
}