namespace Aplikacja.DTOS.UserDtos
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool KeepLoggedIn { get; set; } = false;
    }
}
