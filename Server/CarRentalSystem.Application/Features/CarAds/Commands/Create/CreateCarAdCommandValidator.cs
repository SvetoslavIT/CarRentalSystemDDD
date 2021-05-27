namespace CarRentalSystem.Application.Features.CarAds.Commands.Create
{
    using Common;
    using FluentValidation;

    public class CreateCarAdCommandValidator : AbstractValidator<CreateCarAdCommand>
    {
        public CreateCarAdCommandValidator(ICarAdRepository carAdRepository)
            => Include(new CarAdCommandValidator<CreateCarAdCommand>(carAdRepository));
    }
}