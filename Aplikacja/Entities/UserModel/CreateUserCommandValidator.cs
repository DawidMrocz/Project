
using Aplikacja.DTOS.UserDtos;
using Aplikacja.Models;
using FluentValidation;

namespace Aplikacja.Entities.UserModel
{
    public class CreateUserCommandValidator : AbstractValidator<RegisterDto>
    {
        public CreateUserCommandValidator(ApplicationDbContext contex)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = contex.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is takem");
                }
            });

        }
    }
}
