namespace CarRentalSystem.Application.Features.Dealers.Commands.Edit
{
    using FluentValidation;
    using static Domain.Models.ModelConstants;

    public class EditDealerCommandValidator : AbstractValidator<EditDealerCommand>
    {
        public EditDealerCommandValidator()
        {
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