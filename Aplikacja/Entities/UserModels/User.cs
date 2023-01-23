using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.RaportModels;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja.Entities.UserModel
{
    public class User
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public string? PasswordHash { get; set; }
        public required string Email { get; set; }
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public required string Role { get; set; }
        public byte[]? Photo { get; set; }
        public List<InboxItem>? InboxItems { get; set; } = new List<InboxItem>();
        public List<CatRecord>? CatRecords { get; set; } = new List<CatRecord>();
        public List<UserRaportRecord>? UserRaports { get; set; } = new List<UserRaportRecord>();
    }
}
