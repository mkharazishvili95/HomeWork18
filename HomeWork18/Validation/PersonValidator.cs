using FluentValidation;
using HomeWork18.Models;

namespace HomeWork18.Validation
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() {
            RuleFor(person => person.FirstName).NotEmpty().WithMessage("Enter your FirstName!")
                .Length(1, 50).WithMessage("FirstName length must be between 1 and 50 chars!");
            RuleFor(person => person.LastName).NotEmpty().WithMessage("Enter your LastName!")
                .Length(1, 50).WithMessage("LastName length must be between 1 and 50 chars!");
            RuleFor(person => person.JobPosition).NotEmpty().WithMessage("Enter your Job position!")
                .Length(1, 50).WithMessage("Job position length must be between 1 and 50 chars!");
            RuleFor(person => person.Salary).NotNull().WithMessage("Enter your Salary!");
            RuleFor(person => person.WorkExperience).NotNull().WithMessage("Enter age of your experience for a job!");
            RuleFor(person => person.PersonAddress).SetValidator(new AddressValidator());
        }
    }
}
