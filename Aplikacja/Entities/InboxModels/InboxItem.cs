using Aplikacja.Entities.JobModel;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Entities.InboxModel
{
    public class InboxItem
    {
        public int InboxItemId { get; set; }
        public int Hours { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }
        public string? WhenComplete { get; set; }
        public int JobId { get; set; }
        public Job? Job { get; set; }
        public int? InboxId { get; set; }
        public Inbox? Inbox { get; set; }
    }
}
