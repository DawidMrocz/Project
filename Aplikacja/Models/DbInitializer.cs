

using Aplikacja.Entities.JobModel;
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
                    JobId = 1,
                    JobDescription = "Create drawing",
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
                    Received = "15.22.2022",

                },
                new Job()
                {
                    JobId = 2,
                    JobDescription = "Create drawing",
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
                    Received = "20.11.2022",
                    DueDate = "25.11.2022"
                },
                new Job()
                {
                    JobId = 3,
                    JobDescription = "Create drawing",
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
                    Received = "20.11.2022",
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Dawid",
                    PasswordHash = "zxcasdqwe",
                    Email = "dawid@tlen.pl",
                    CCtr = "RS8531",
                    ActTyp = "L8531",
                    Role = "Admin",
                    Photo = "zdjecie"
                },
                new User
                {
                    UserId = 2,
                    Name = "Dawid2",
                    PasswordHash = "zxcasdqwe",
                    Email = "dawid2@tlen.pl",
                    CCtr = "RS8531",
                    ActTyp = "L8531",
                    Role = "Manager",
                    Photo = "zdjecie"
                }
            );
        }
    }
}
