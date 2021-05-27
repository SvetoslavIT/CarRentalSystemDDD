namespace CarRentalSystem.Application.Features.Identity.Commands.ChangePassword
{
    using FluentValidation;
    using static Domain.Models.ModelConstants;

    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(u => u.CurrentPassword)
                .MinimumLength(PasswordMinLength)
                .MaximumLength(PasswordMaxLength)
                .NotEmpty();

            RuleFor(u => u.NewPassword)
                .MinimumLength(PasswordMinLength)
                .MaximumLength(PasswordMaxLength)
                .NotEmpty();
        }
    }
}