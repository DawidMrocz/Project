using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.RaportModels;

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
        public Inbox Inbox { get; set; }
        public List<Cat>? Cats { get; set; }
        public List<UserRaport>? UserRaports { get; set; }
        //public User()
        //{
        //    this.Inbox = new Inbox();
        //    this.Cats = new List<Cat>();
        //    this.UserRaports = new List<UserRaport>();
        //}
    }
}
