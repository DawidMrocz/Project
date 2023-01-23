using Aplikacja.DTOS.UserDtos;
using Aplikacja.Models;
using FluentValidation;

namespace Aplikacja.Entities.RaportModels
{
    public class CreateRaportCommandValidator : AbstractValidator<Raport>
    {
        public CreateRaportCommandValidator(ApplicationDbContext contex)
        {
            RuleFor(x => x.Year).LessThan(2100).GreaterThan(2023).NotEmpty();
            RuleFor(x => x.Month).LessThanOrEqualTo(12).GreaterThanOrEqualTo(1);
        }
    }
}
