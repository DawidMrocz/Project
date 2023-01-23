using Aplikacja.Models;
using FluentValidation;

namespace Aplikacja.Entities.RaportModels
{
    public class CreateUserRaportRecordCommandValidator : AbstractValidator<UserRaportRecord>
    {
        public CreateUserRaportRecordCommandValidator(ApplicationDbContext contex)
        {
            RuleFor(x => x.TaskHours).GreaterThanOrEqualTo(0).LessThanOrEqualTo(200).NotEmpty();
        }
    }
}
