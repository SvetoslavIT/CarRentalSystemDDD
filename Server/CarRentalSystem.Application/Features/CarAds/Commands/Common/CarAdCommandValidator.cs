namespace CarRentalSystem.Application.Features.CarAds.Commands.Common
{
    using System;
    using CarRentalSystem.Domain.Common;
    using CarRentalSystem.Domain.Models.CarAds;
    using FluentValidation;
    using static Domain.Models.ModelConstants;

    public class CarAdCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : CarAdCommand
    {
        public CarAdCommandValidator(ICarAdRepository carAdRepository)
        {
            RuleFor(c => c.Manufacturer)
                .MinimumLength(NameMinLength)
                .MaximumLength(NameMaxLength)
                .NotEmpty();

            RuleFor(c => c.Model)
                .MinimumLength(ModelMinLength)
                .MaximumLength(ModelMaxLength)
                .NotEmpty();

            RuleFor(c => c.Category)
                .MustAsync(async (category, token) => await carAdRepository
                    .GetCategory(category, token) != null)
                .WithMessage("'{PropertyName}' does not exist.");

            RuleFor(c => c.ImageUrl)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("'{PropertyName}' must be a valid url.")
                .NotEmpty();

            RuleFor(c => c.PricePerDay)
                .InclusiveBetween(MinPricePerDay, MaxPricePerDay);

            RuleFor(c => c.NumberOfSeats)
                .InclusiveBetween(MinNumberOfSeats, MaxNumberOfSeats);

            RuleFor(c => c.TransmissionType)
                .Must(Enumeration.BeAValid<TransmissionType>)
                .WithMessage("'{PropertyName}' is not a valid transmission type.");
        }
    }
}