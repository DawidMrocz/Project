

using Aplikacja.Entities.UserModel;
using AutoMapper;

namespace Aplikacja.DTOS.UserDtos
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
