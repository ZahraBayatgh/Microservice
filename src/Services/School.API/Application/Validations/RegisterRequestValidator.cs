using API.Models;
using Domain.AggregatesModel.StudentAggregate;
using FluentValidation;

namespace API.Application.Validations
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(0, 200);

            When(x => x.Email == null, () =>
            {
                RuleFor(x => x.Email).NotEmpty();
            });
            When(x => x.Phone == null, () =>
            {
                RuleFor(x => x.Phone).NotEmpty();
            });

            RuleFor(x => x.Email)
                .MustBeValueObject(Email.Create)
                .When(x => x.Email != null);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches("^[2-9][0-9]{9}$")
                .When(x => x.Phone != null);
        }
    }
}
