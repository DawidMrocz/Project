

using Aplikacja.DTOS.InboxDtos;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.UserModel;
using AutoMapper;

namespace Aplikacja.DTOS.UserDtos
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<InboxItem, InboxItemDTO>();
        }
    }
}
