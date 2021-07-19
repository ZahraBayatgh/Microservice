using API.Models;
using Domain.AggregatesModel.StudentAggregate;
using FluentValidation;

namespace API.Application.Validations
{
    public class EnrollRequestValidator : AbstractValidator<EnrollRequest>
    {
        public EnrollRequestValidator()
        {
            RuleFor(x => x.Enrollments)
                .NotEmpty()
                .ListMustContainNumberOfItems(min: 1)
                .ForEach(x =>
                {
                    x.NotNull();
                    x.ChildRules(enrollment =>
                    {
                        enrollment.RuleFor(y => y.Course).NotEmpty().Length(0, 100);
                        enrollment.RuleFor(y => y.Grade).MustBeValueObject(Grade.Create);
                    });
                });
        }
    }
}
