using Aplikacja.Entities.UserModel;

namespace Aplikacja.Entities.RaportModels
{
    public class UserRaport
    {
        public int UserRaportId { get; set; }
        public double UserAllHours { get; set; } = 0;
        public int RaportId { get; set; }
        public Raport Raport { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } 
        public List<UserRaportRecord> UserRaportRecords { get; set; }
        public UserRaport()
        {
            this.UserRaportRecords = new List<UserRaportRecord>();
        }
    }
}
