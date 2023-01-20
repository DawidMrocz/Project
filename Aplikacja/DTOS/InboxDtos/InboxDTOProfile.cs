using Aplikacja.DTOS.CatDto;
using Aplikacja.Entities.CatModels;
using Aplikacja.Entities.InboxModel;
using AutoMapper;

namespace Aplikacja.DTOS.InboxDtos
{
    public class InboxDTOProfile:Profile
    {
        public InboxDTOProfile()
        {
            CreateMap<Inbox, InboxDTO>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.User.Name))
                .ForMember(m => m.Photo, c => c.MapFrom(s => s.User.Photo))
                .ForMember(m => m.Email, c => c.MapFrom(s => s.User.UserId))
                .ForMember(m => m.Role, c => c.MapFrom(s => s.User.CCtr));

            CreateMap<InboxItem, InboxItemDTO>();
        }
    }
}
