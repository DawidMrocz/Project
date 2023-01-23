using Aplikacja.DTOS.InboxDtos;
using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;
using AutoMapper;

namespace Aplikacja.DTOS.CatDto
{
    public class CatRecordDTOProfile:Profile
    {
        public CatRecordDTOProfile()
        {
            CreateMap<CatRecord, CatRecordDTO>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.User.Name))
                .ForMember(m => m.CCtr, c => c.MapFrom(s => s.User.CCtr))
                .ForMember(m => m.ActTyp, c => c.MapFrom(s => s.User.ActTyp))
                .ForMember(m => m.Role, c => c.MapFrom(s => s.User.Role))
                .ForMember(m => m.Photo, c => c.MapFrom(s => s.User.Photo));

            CreateMap<CatRecordHours, CatRecordHoursDTO>();
        }
    }
}
