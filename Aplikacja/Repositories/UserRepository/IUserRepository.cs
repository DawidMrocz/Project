using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.UserModel;

namespace Aplikacja.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> GetProfile(int userId);

        public Task<bool> LoginUser(LoginDto command);
        public Task<String> ForgotPassword();
        public Task<String> ChangePassword();
        public Task<String> ChangeRole();


        public Task<User> CreateUser(RegisterDto command);
        public Task<bool> DeleteUser(int userId);
        public Task<User> UpdateUser(UpdateDto command, int userId);
    }
}
