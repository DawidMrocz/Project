using Aplikacja.DTOS.RaportDto;
using Aplikacja.Entities.RaportModels;
using AutoMapper;

namespace Aplikacja.DTOS.RaportDto
{
    public class UserRaportDTOProfile:Profile
    {
        public UserRaportDTOProfile() 
        {
            CreateMap<UserRaport, UserRaportDTO>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.User.Name))
                .ForMember(m => m.Photo, c => c.MapFrom(s => s.User.Photo))
                .ForMember(m => m.Role, c => c.MapFrom(s => s.User.Role));

            CreateMap<UserRaportRecord, UserRaportRecordDTO>();
        }
    }
}