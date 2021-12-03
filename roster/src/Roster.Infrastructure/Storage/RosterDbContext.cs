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

        public DbSet<Dlc> Dlcs { get; set; }

        public DbSet<Rank> Ranks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationForm>()
                        .HasKey(af => af.Nickname);

            modelBuilder.Entity<ApplicationForm>()
                        .Property(af => af.Nickname)
                        .IsRequired()
                        .HasConversion<string>(v => v.Nickname, v => new MemberNickname(v))
                        .HasColumnName("Nickname");
                        
            modelBuilder.Entity<ApplicationForm>()
                        .OwnsMany<OwnedDlc>(af => af.OwnedDlcs);

            modelBuilder.Entity<ApplicationForm>()
                        .OwnsOne<EmailAddress>(af => af.Email)
                        .Property(a => a.Email)
                        .HasColumnName("Email");

            modelBuilder.Entity<ApplicationForm>()
                        .OwnsOne<EmailAddress>(af => af.Gmail)
                        .Property(a => a.Email)
                        .HasColumnName("Gmail");

            modelBuilder.Entity<ApplicationForm>()
                        .OwnsOne<DiscordId>(af => af.DiscordId)
                        .Property(d => d.Id)
                        .HasColumnName("DiscordId");

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

            modelBuilder.Entity<Member>()
                        .Property(m => m.RankId)
                        .HasConversion<int>(rank => rank.Id, id => new RankId(id))
                        .HasColumnName("RankId");

            modelBuilder.Entity<Dlc>()
                        .HasKey(d => d.DlcName);

            modelBuilder.Entity<Dlc>()
                        .Property(d => d.DlcName)
                        .IsRequired()
                        .HasConversion<string>(dlc => dlc.Name, name => new DlcName(name))
                        .HasColumnName("Name");

            modelBuilder.Entity<Rank>()
                        .HasKey("_id");

            modelBuilder.Entity<Rank>()
                        .Property("_id")
                        .IsRequired()
                        .HasColumnName("Id")
                        .HasIdentityOptions(startValue: 1);

            modelBuilder.Entity<Rank>()
                        .Property(r => r.Name)
                        .IsRequired();
        }
    }
}