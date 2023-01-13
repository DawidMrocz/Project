namespace Aplikacja.Entities.CatModels
{
    public class CatRecordHours
    {
        public int CatRecordHoursId { get; set; }
        public int? Day { get; set; }
        public double Hours { get; set; }
        public int CatRecordId { get; set; }
        public CatRecord? CatRecord { get; set; }
    }
}
