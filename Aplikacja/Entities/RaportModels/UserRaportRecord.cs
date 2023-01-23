using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.Entities.RaportModels
{
    public class UserRaportRecord
    {
        public Guid UserRaportRecordId { get; set; }
        public required double TaskHours { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid RaportId { get; set; }
        public Raport? Raport { get; set; }

        public Guid InboxItemId { get; set; }
        public InboxItem? InboxItem { get; set; }   
    }
}
