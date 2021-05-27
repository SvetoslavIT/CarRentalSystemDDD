namespace CarRentalSystem.Application.Features.CarAds.Commands.Edit
{
    using Common;
    using FluentValidation;

    public class EditCarAdCommandValidator : AbstractValidator<EditCarAdCommand>
    {
        public EditCarAdCommandValidator(ICarAdRepository carAdRepository)
            => Include(new CarAdCommandValidator<EditCarAdCommand>(carAdRepository));
    }
}