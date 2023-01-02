
using Aplikacja.DTOS.UserDtos;

using Aplikacja.Entities.UserModel;
using Aplikacja.Extensions;
using Aplikacja.Models;
using Aplikacja.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aplikacja.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDistributedCache _cache;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserRepository(
            ApplicationDbContext context,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IDistributedCache cache,
            AuthenticationSettings authenticationSettings
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));
        }

        public async Task<UserDto> GetProfile(int userId)
        {
            User? profile = await _cache.GetRecordAsync<User>($"Profile_{userId}");
            if (profile is null)
            {
                profile = await _context.Users.AsNoTracking().SingleAsync(p => p.UserId == userId);
                await _cache.SetRecordAsync($"Profile_{userId}", profile);
            }
            return _mapper.Map<UserDto>(profile);
        }
        public async Task<User> CreateUser(RegisterDto command)
        {
            User newUser = new User()
            {
                Name = command.Name,
                Email = command.Email,
                ActTyp = command.ActTyp,
                CCtr = command.CCtr,
                Photo = command.Photo,
                Role = "User",
               // Cats = new List<Cat>(),
               // UserRaports = new List<UserRaport>(), 
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, command.Password);
            await _context.Users.AddAsync(newUser);

            //Inbox newInbox = new Inbox()
            //{
            //    UserId = newUser.UserId
            //};

            //await _context.Inboxs.AddAsync(newInbox);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<String> LoginUser(LoginDto command)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == command.Email);
            if (user is null) throw new BadHttpRequestException("Bad");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);
            if (result == PasswordVerificationResult.Failed) throw new BadHttpRequestException("Bad");
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim("CCtr", user.CCtr.ToString()),
                    new Claim("ActTyp", user.ActTyp.ToString()),
                    new Claim("Photo", user.Photo.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public async Task<bool> DeleteUser(int userId)
        {
            var myProfile = await _context.Users.SingleAsync(r => r.UserId == userId);
            if (myProfile is null) throw new BadHttpRequestException("Bad");
            await _cache.DeleteRecordAsync<User>($"Profile_{userId}");
            _context.Remove(myProfile);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> UpdateUser(UpdateDto command,int userId)
        {
            var currentUser = await _context.Users.SingleAsync(r => r.UserId == userId);
            if (currentUser is null) throw new BadHttpRequestException("Bad");
            currentUser.Name = command.Name;
            currentUser.Email = command.Email;
            currentUser.CCtr = command.CCtr;
            currentUser.ActTyp = command.ActTyp;
            currentUser.Photo = command.Photo;

            await _cache.DeleteRecordAsync<User>($"Profile_{userId}");
            _context.SaveChanges();
            return currentUser;
        }

        public Task<string> ForgotPassword()
        {
            throw new NotImplementedException();
        }

        public Task<string> ChangePassword()
        {
            throw new NotImplementedException();
        }

        public Task<string> ChangeRole()
        {
            throw new NotImplementedException();
        }
    }
}
