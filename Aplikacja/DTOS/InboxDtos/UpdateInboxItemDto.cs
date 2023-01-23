using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.DTOS.InboxDtos
{
    public class UpdateInboxItemDto
    {
        public double Hours { get; set; }
        public string? Status { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
    }
}
