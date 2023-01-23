namespace Aplikacja.Entities.CatModels
{
    public class CatRecordHours
    {
        public Guid CatRecordHoursId { get; set; }
        public int? Day { get; set; }
        public double Hours { get; set; }
        public Guid CatRecordId { get; set; }
        public CatRecord? CatRecord { get; set; }
    }
}
