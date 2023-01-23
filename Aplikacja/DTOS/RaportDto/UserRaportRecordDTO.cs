
namespace Aplikacja.DTOS.RaportDto
{
    public class UserRaportRecordDTO
    {
        public Guid UserRaportRecordId { get; set; }
        public double TaskHours { get; set; }
        public required double Hours { get; set; }
        public required int Components { get; set; }
        public required int DrawingsComponents { get; set; }
        public required int DrawingsAssembly { get; set; }
        public required string System { get; set; }
        public required int Ecm { get; set; }
        public int? Gpdm { get; set; }
        public string? Client { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
    }
}
