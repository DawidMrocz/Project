using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.UserModel;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> GetProfile(int userId);

        public Task<bool> LoginUser(LoginDto command);
        public Task<bool> ForgotPassword(string userEmail);
        public Task<bool> ChangePassword(int userId, string oldPassword, string newPassword, string newPasswordRepeat);
        public Task<bool> ChangeRole(int userId, string role);


        public Task<User> CreateUser(RegisterDto command);
        public Task<bool> DeleteUser(int userId);
        public Task<User> UpdateUser(UpdateDto command, int userId);
    }
}
