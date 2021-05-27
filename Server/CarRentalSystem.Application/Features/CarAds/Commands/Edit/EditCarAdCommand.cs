namespace CarRentalSystem.Application.Features.CarAds.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Contracts;
    using Common;
    using Dealers;
    using CarRentalSystem.Domain.Common;
    using CarRentalSystem.Domain.Models.CarAds;
    using MediatR;

    public class EditCarAdCommand : CarAdCommand, IRequest<Result>
    {
        public class EditCarAdCommandHandler : IRequestHandler<EditCarAdCommand, Result>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IDealerRepository _dealerRepository;
            private readonly ICarAdRepository _carAdRepository;

            public EditCarAdCommandHandler(
                ICurrentUser currentUser, 
                IDealerRepository dealerRepository, 
                ICarAdRepository carAdRepository)
            {
                _currentUser = currentUser;
                _dealerRepository = dealerRepository;
                _carAdRepository = carAdRepository;
            }

            public async Task<Result> Handle(EditCarAdCommand request, CancellationToken cancellationToken)
            {
                var result = await _currentUser
                    .DealerHasCarAd(_dealerRepository, request.Id, cancellationToken);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                var carAd = await _carAdRepository.Find(request.Id, cancellationToken);
                var manufacturer = await _carAdRepository
                    .GetManufacturer(request.Manufacturer, cancellationToken);

                carAd = manufacturer == null
                    ? carAd.UpdateManufacturer(request.Manufacturer)
                    : carAd.UpdateManufacturer(manufacturer);

                var category = await _carAdRepository
                    .GetCategory(request.Category, cancellationToken);

                carAd
                    .UpdateModel(request.Model)
                    .UpdateCategory(category)
                    .UpdateImageUrl(request.ImageUrl)
                    .UpdatePricePerDay(request.PricePerDay)
                    .UpdateOptions(
                        request.HasClimateControl,
                        request.NumberOfSeats,
                        Enumeration.FromValue<TransmissionType>(request.TransmissionType));

                await _carAdRepository.Save(carAd, cancellationToken);

                return result;
            }
        }
    }
}