using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Entities.UserModel;
using Aplikacja.Extensions;
using Aplikacja.Models;
using Aplikacja.Settings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MimeKit;
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
            List<UserDto>? users = await _cache.GetRecordAsync<List<UserDto>>("AllUsers");
            if (users is null)
            {
                users = await _context.Users.AsNoTracking().OrderBy(n => n.Name).ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
                await _cache.SetRecordAsync("AllUsers", users);
            }
            return users;
        }

        public async Task<UserDto> GetProfile(int userId)
        {
            UserDto? profile = await _cache.GetRecordAsync<UserDto>($"Profile_{userId}");
            if (profile is null)
            {
                profile = await _context.Users.AsNoTracking().ProjectTo<UserDto>(_mapper.ConfigurationProvider).SingleAsync(p => p.UserId == userId);

                await _cache.SetRecordAsync($"Profile_{userId}", profile);
            }
            return profile;
        }
        public async Task<User> CreateUser(RegisterDto command)
        {

            User newUser = new User()
            {
                Name = command.Name,
                Email = command.Email,
                ActTyp = command.ActTyp,
                CCtr = command.CCtr,
                Role = "User",
                Inbox = new Inbox()
            };

            if (command.ProfilePhoto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (memoryStream.Length < 2097152)
                    {
                        await command.ProfilePhoto.CopyToAsync(memoryStream);
                        newUser.Photo = memoryStream.ToArray();
                    }
                   
                }
            }

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
            var resultQuantity = await _context.Users.Where(u => u.UserId == userId).ExecuteDeleteAsync();
            if (resultQuantity != 1) throw new BadHttpRequestException("Bad");
            await _cache.DeleteRecordAsync<User>($"Profile_{userId}");
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
            //currentUser.Photo = command.Photo;

            await _cache.DeleteRecordAsync<User>($"Profile_{userId}");
            _context.SaveChanges();
            return currentUser;
        }

        public async Task<bool> ForgotPassword(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user is null) throw new BadHttpRequestException("Bad");

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var newPassword = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ellen81@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = "Tekst email sub";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = newPassword };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email",587,SecureSocketOptions.StartTls);
            smtp.Authenticate("ellen81@ethereal.email", "ZjgDjPpsKN6WaFRrWz");
            smtp.Send(email);
            smtp.Disconnect(true);
            return true;
        }

        public async Task<bool> ChangePassword(int userId,string oldPassword,string newPassword, string newPasswordRepeat)
        {
            if (newPassword != newPasswordRepeat) throw new BadHttpRequestException("New passwords are not the same");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user is null) throw new BadHttpRequestException("Bad");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);

            if (result == PasswordVerificationResult.Failed) throw new BadHttpRequestException("Bad");

            await _context.Users.Where(u => u.UserId == userId).ExecuteUpdateAsync(
                s => s.SetProperty(b => b.PasswordHash, b => _passwordHasher.HashPassword(user, newPassword)));

            return true;
        }

        public async Task<bool> ChangeRole(int userId,string role)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user is null) throw new BadHttpRequestException("Bad");

            await _context.Users.Where(u => u.UserId == userId).ExecuteUpdateAsync(
                s => s.SetProperty(b => b.Role, b => role));

            return true;
        }
    }
}
