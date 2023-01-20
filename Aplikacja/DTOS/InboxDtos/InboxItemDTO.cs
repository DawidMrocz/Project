
namespace Aplikacja.DTOS.InboxDtos
{
    public class InboxItemDTO
    {
        public int InboxItemId { get; set; }
        public int Hours { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }
        public string? WhenComplete { get; set; }
        public int? Ecm { get; set; }
        public int? Gpdm { get; set; }
        public string? DueDate { get; set; }
        public string? Started { get; set; }
        public string? Finished { get; set; }
        public string? Link { get; set; }
        public string? JobDescription { get; set; }
        public string? Status { get; set; }
    }
}
