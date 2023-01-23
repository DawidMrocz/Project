using Aplikacja.Entities.RaportModels;
using AutoMapper;

namespace Aplikacja.DTOS.RaportDto
{
    public class RaportDTOProfile:Profile
    {
        public RaportDTOProfile()
        {
            CreateMap<Raport, RaportDTO>();
            CreateMap<UserRaportRecord, UserRaportRecordDTO>();    
        }
    }
}
