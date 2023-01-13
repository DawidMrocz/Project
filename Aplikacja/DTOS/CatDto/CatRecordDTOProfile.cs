using Aplikacja.Entities.CatModels;
using AutoMapper;

namespace Aplikacja.DTOS.CatDto
{
    public class CatRecordDTOProfile:Profile
    {
        public CatRecordDTOProfile()
        {
            CreateMap<CatRecordHours, CatRecordHoursDTO>();
        }
    }
}
