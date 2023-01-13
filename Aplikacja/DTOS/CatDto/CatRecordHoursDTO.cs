using Aplikacja.Entities.CatModels;

namespace Aplikacja.DTOS.CatDto
{
    public class CatRecordHoursDTO
    {
        public int CatRecordHoursId { get; set; }
        public int? Day { get; set; }
        public double Hours { get; set; }
    }
}
