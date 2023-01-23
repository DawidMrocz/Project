using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;

namespace Aplikacja.Entities.RaportModels
{
    public class Raport
    {
        public Guid RaportId { get; set; }
        public required int Year { get; set; }
        public required int Month { get; set; }
        public List<UserRaportRecord>? UserRaportRecords { get; set; } = new List<UserRaportRecord>();
    }
}
