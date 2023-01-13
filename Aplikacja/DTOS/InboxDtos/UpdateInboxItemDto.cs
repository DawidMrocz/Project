using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.DTOS.InboxDtos
{
    public class UpdateInboxItemDto
    {
        public int Hours { get; set; }
        public string Status { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }
        public string WhenComplete { get; set; }
        public string Started { get; set; }
        public string Finished { get; set; }
    }
}
