using Aplikacja.Entities.CatModels;
using AutoMapper;

namespace Aplikacja.DTOS.CatDto
{
    public class CatDTOProfile:Profile
    {
        public CatDTOProfile()
        {
            CreateMap<Cat, CatDTO>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.User.Name))
                .ForMember(m => m.Photo, c => c.MapFrom(s => s.User.Photo))
                .ForMember(m => m.ActTyp, c => c.MapFrom(s => s.User.ActTyp))
                .ForMember(m => m.CCtr, c => c.MapFrom(s => s.User.CCtr));

            CreateMap<CatRecord, CatRecordDTO>();
        }
    }
}
