

using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.JobModel;
using Aplikacja.Entities.RaportModels;
using Aplikacja.Entities.UserModel;
using Microsoft.EntityFrameworkCore;

namespace Aplikacja.Models
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;
        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            modelBuilder.Entity<Job>().HasData(
                new Job()
                {   
                    JobId=1,
                    JobDescription = "Create Muffler",
                    Type = "2D",
                    System = "Catia",
                    Link = "linkt o task",
                    Engineer = "Agata",
                    Ecm = 4561976,
                    Gpdm = 1,
                    ProjectNumber = "LASDl",
                    Client = "TOYOTA",
                    ProjectName = "sap text",
                    Status = "2D",
                },
                new Job()
                {
                    JobId = 2,
                    JobDescription = "Create drawing",
                    Type = "3D",
                    System = "Catia",
                    Link = "linkt o task",
                    Engineer = "Agata",
                    Ecm = 4561976,
                    Gpdm = 1,
                    ProjectNumber = "LASDl",
                    Client = "TOYOTA",
                    ProjectName = "sap text",
                    Status = "2D",
                },
                new Job()
                {
                    JobId = 3,
                    JobDescription = "Update hangers",
                    Type = "2D",
                    System = "Catia",
                    Link = "linkt o task",
                    Engineer = "Agata",
                    Ecm = 4561976,
                    Gpdm = 1,
                    ProjectNumber = "LASDl",
                    Client = "TOYOTA",
                    ProjectName = "sap text",
                    Status = "2D",
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId= Guid.NewGuid(),
                    Name = "Dawid",
                    PasswordHash = "zxcasdqwe",
                    Email = "dawid@tlen.pl",
                    CCtr = "RS8531",
                    ActTyp = "L8531",
                    Role = "Admin",
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Agata",
                    PasswordHash = "zxcasdqwe",
                    Email = "agata@tlen.pl",
                    CCtr = "RS8531",
                    ActTyp = "L8531",
                    Role = "Manager",
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Dawid",
                    PasswordHash = "zxcasdqwe",
                    Email = "dawid@tlen.pl",
                    CCtr = "RS8531",
                    ActTyp = "L8531",
                    Role = "Admin",
                },
                new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Agata",
                    PasswordHash = "zxcasdqwe",
                    Email = "agata@tlen.pl",
                    CCtr = "RS8531",
                    ActTyp = "L8531",
                    Role = "Manager",
                }
            );
        }
    }
}
