using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.DTOS.CatDto
{
    public class CatRecordDTO
    {
        public Guid CatRecordId { get; set; }
        public required int Month { get; set; }
        public required int Year { get; set; }
        public string? Receiver { get; set; }
        public string? SapText { get; set; }
        public List<CatRecordHoursDTO>? CatRecordHours { get; set; }

        // FROM USER SIDE
        public required string Name { get; set; }
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public required string Role { get; set; }
        public byte[]? Photo { get; set; }
        
    }
}

