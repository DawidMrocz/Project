using Aplikacja.Entities.UserModel;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Entities.InboxModel
{
    public class Inbox
    {
        public int InboxId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<InboxItem>? InboxItems { get; set; }
    }
}
