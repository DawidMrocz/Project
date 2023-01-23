﻿namespace Aplikacja.DTOS.UserDtos
{
    public class RegisterDto
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}
