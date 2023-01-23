using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.UserModel;
using Microsoft.AspNetCore.Mvc;

namespace Aplikacja.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> GetProfile(Guid userId);

        public Task<bool> LoginUser(LoginDto command);
        public Task<bool> ForgotPassword(string userEmail);
        public Task<bool> ChangePassword(Guid userId, string oldPassword, string newPassword, string newPasswordRepeat);
        public Task<bool> ChangeRole(Guid userId, string role);


        public Task<User> CreateUser(RegisterDto command);
        public Task<bool> DeleteUser(Guid userId);
        public Task<User> UpdateUser(UpdateDto command, Guid userId);
    }
}
