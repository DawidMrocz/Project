using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.Entities.CatModels
{
    public class CatRecord
    {
        public Guid CatRecordId { get; set; }
        public string? Receiver { get; set; }
        public string? SapText { get; set; }
        public required int Month { get; set; }
        public required int Year { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid InboxItemId { get; set; }
        public InboxItem? InboxItem { get; set; }
        public List<CatRecordHours> CatRecordHours { get; set; } = new List<CatRecordHours>();
    }
}
