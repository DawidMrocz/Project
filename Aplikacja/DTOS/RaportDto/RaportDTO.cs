using Aplikacja.Entities.RaportModels;

namespace Aplikacja.DTOS.RaportDto
{
    public class RaportDTO
    {
        public int RaportId { get; set; }
        public double? TotalHours { get; set; }
        public string? Created { get; set; }
        public List<UserRaportDTO>? UserRaports { get; set; }

    }
}
