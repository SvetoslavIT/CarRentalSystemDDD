namespace CarRentalSystem.Application.Features.Identity.Commands.CreateUser
{
    using FluentValidation;
    using static Domain.Models.ModelConstants;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .MinimumLength(EmailMinLength)
                .MaximumLength(EmailMaxLength)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .MinimumLength(PasswordMinLength)
                .MaximumLength(PasswordMaxLength)
                .NotEmpty();

            RuleFor(u => u.Name)
                .MinimumLength(NameMinLength)
                .MaximumLength(NameMaxLength)
                .NotEmpty();

            RuleFor(u => u.PhoneNumber)
                .MinimumLength(PhoneNumberMinLength)
                .MaximumLength(PhoneNumberMaxLength)
                .NotEmpty()
                .Matches(PhoneNumberPattern)
                .WithMessage("'{PropertyName}' must be start with '+' and following with numbers.");
        }
    }
}