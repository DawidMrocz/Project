using Aplikacja.Entities.JobModel;
using Aplikacja.Entities.UserModel;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Entities.InboxModel
{
    public class InboxItem
    {
        public Guid InboxItemId { get; set; }
        public double Hours { get; set; } = 0;
        public int Components { get; set; } = 0;
        public int DrawingsComponents { get; set; } = 0;
        public int DrawingsAssembly { get; set; } = 0;
        public int JobId { get; set; }
        public Job? Job { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
