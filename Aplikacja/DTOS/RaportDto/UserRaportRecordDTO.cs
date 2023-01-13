using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.RaportModels;

namespace Aplikacja.DTOS.RaportDto
{
    public class UserRaportRecordDTO
    {
        public int UserRaportRecordId { get; set; }
        public double TaskHours { get; set; }
        public int Hours { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }
        public string? System { get; set; }
        public int? Ecm { get; set; }
        public int? Gpdm { get; set; }
        public string? Client { get; set; }
        public string? DueDate { get; set; }
        public string? Started { get; set; }
        public string? Finished { get; set; }
    }
}
