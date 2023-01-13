using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.Entities.CatModels
{
    public class Cat
    {
        public int CatId { get; set; }
        public string CatCreated { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<CatRecord>? CatRecords { get; set; }
        public Cat()
        {
            this.CatRecords = new List<CatRecord>();
        }
    }
}
