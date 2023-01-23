

using Aplikacja.Entities.InboxModel;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja.Entities.JobModel
{
    public class Job
    {
        public int JobId { get; set; }
        public string? JobDescription { get; set; } = "No description";
        [Required]
        public required string Type { get; set; } 
        public required string System { get; set; }
        [Url]
        public string? Link { get; set; }
        [EmailAddress]
        public string? Engineer { get; set; }
        public required int Ecm { get; set; }
        public int? Gpdm { get; set; }
        public string? Region { get; set; } = "NA";
        public string? ProjectNumber { get; set; }
        public string? Client { get; set; }
        public string? ProjectName { get; set; }
        public string Status { get; set; } = "TBD";
        public DateTime? Received { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public DateTime? Started { get; set; } = null;
        public DateTime? Finished { get; set; } = null;
        public List<InboxItem>? InboxItems { get; set; }
    }
}
