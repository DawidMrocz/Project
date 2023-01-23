using Aplikacja.Entities.RaportModels;

namespace Aplikacja.DTOS.RaportDto
{
    public class RaportDTO
    {
        public Guid RaportId { get; set; }
        public required int Year { get; set; }
        public required int Month { get; set; }
        public List<UserRaportRecordDTO>? UserRaports { get; set; }

    }
}
