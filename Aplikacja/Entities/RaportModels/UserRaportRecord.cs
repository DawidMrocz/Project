using Aplikacja.Entities.InboxModel;

namespace Aplikacja.Entities.RaportModels
{
    public class UserRaportRecord
    {
        public int UserRaportRecordId { get; set; }
        public double TaskHours { get; set; }
        public int UserRaportId { get; set; }
        public UserRaport? UserRaport { get; set; }
        public int InboxItemId { get; set; }
        public InboxItem? InboxItem { get; set; }   
    }
}
