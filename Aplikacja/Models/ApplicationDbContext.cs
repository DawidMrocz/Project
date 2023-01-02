

using Aplikacja.Entities.JobModel;
using Aplikacja.Entities.UserModel;
using Microsoft.EntityFrameworkCore;

namespace Aplikacja.Models
{
    public class ApplicationDbContext : DbContext
    {
       public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        //public DbSet<Inbox> Inboxs { get; set; }
        //public DbSet<InboxItem> InboxItems { get; set; }
        //public DbSet<Raport> Raports { get; set; }
        //public DbSet<UserRaport> UserRaports { get; set; }
        //public DbSet<UserRaportRecord> UserRaportRecords { get; set; }
        //public DbSet<Cat> Cats { get; set; }
        //public DbSet<CatRecord> CatRecords { get; set; }
        //public DbSet<CatRecordHours> CatRecordHourss { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            new DbInitializer(modelBuilder).Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableDetailedErrors();
        }
    }
}
