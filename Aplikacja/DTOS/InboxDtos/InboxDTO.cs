

namespace Aplikacja.DTOS.InboxDtos
{
    public class InboxDTO
    {
        public int InboxId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public byte[]? Photo { get; set; }
        public string? Role { get; set; }
        public List<InboxItemDTO>? InboxItems { get; set; }
    }
}
