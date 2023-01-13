using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.UserModel;
using Aplikacja.Extensions;
using Aplikacja.Models;
using Aplikacja.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;

namespace Aplikacja.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDistributedCache _cache;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(
            ApplicationDbContext context,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IDistributedCache cache,
            AuthenticationSettings authenticationSettings,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<List<UserDto>> GetUsers()
        {
            List<User>? users = await _cache.GetRecordAsync<List<User>>("AllUsers");
            if (users is null)
            {
                users = await _context.Users.AsNoTracking().OrderBy(n => n.Name).ToListAsync();
                await _cache.SetRecordAsync("AllUsers", users);
            }
            return _mapper.Map<List<UserDto>>(users);
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
                Inbox = new Inbox()
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, command.Password);
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<bool> LoginUser(LoginDto command)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == command.Email);

            if (user is null) throw new BadHttpRequestException("Bad");
         
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);

            if (result == PasswordVerificationResult.Failed) throw new BadHttpRequestException("Bad");

            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,user.Role),
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = command.KeepLoggedIn
            };
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return true;
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
