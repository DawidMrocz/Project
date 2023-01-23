using Aplikacja.DTOS.CatDto;
using Aplikacja.DTOS.InboxDtos;
using Aplikacja.DTOS.RaportDto;
using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.RaportModels;

namespace Aplikacja.DTOS.UserDtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public required string Role { get; set; }
        public byte[]? Photo { get; set; }
        public List<InboxItemDTO>? InboxItems { get; set; }
        public List<CatRecordDTO>? CatRecords { get; set; } = new List<CatRecordDTO>();
        public List<UserRaportRecordDTO>? UserRaports { get; set; } = new List<UserRaportRecordDTO>();
    }
}


