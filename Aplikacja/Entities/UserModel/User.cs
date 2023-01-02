using Aplikacja.Entities.JobModel;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Entities.UserModel
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? CCtr { get; set; }
        public string? ActTyp { get; set; }
        public string? Role { get; set; }
        public string? Photo { get; set; }


        //JOBS
        public int JobId { get; set; }
        public Job? Job { get; set; }

        //INBOX
       // public Inbox? Inbox { get; set; }

        //CATS
       // public List<Cat>? Cats { get; set; }

        //RAPORTS
       // public List<UserRaport>? UserRaports { get; set; }
    }
}
