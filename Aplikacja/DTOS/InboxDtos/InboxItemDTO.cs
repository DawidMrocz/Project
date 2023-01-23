
namespace Aplikacja.DTOS.InboxDtos
{
    public class InboxItemDTO
    {
        public Guid InboxItemId { get; set; }
        public double Hours { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }

        //FROM USER SIDE
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public byte[]? Photo { get; set; }

        //FROM JOB SIDE
        public int? Ecm { get; set; }
        public int? Gpdm { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
        public string? Link { get; set; }
        public string? JobDescription { get; set; }
        public string? Status { get; set; }
    }
}
