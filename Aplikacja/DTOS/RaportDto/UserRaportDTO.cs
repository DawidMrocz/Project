using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.RaportModels;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.DTOS.RaportDto
{
    public class UserRaportDTO
    {
        public int UserRaportId { get; set; }
        public double UserAllHours { get; set; } = 0;
        public string? Name { get; set; }
        public string? Photo { get; set; }
        public string? Role { get; set; }
        public List<UserRaportRecordDTO> UserRaportRecords { get; set; }
    }
}
