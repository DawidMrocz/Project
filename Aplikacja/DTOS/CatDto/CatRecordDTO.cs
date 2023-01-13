using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;

namespace Aplikacja.DTOS.CatDto
{
    public class CatRecordDTO
    {
        public int CatRecordId { get; set; }
        public string? Receiver { get; set; }
        public string? SapText { get; set; }
        public List<CatRecordHoursDTO>? CatRecordHours { get; set; }
    }
}
