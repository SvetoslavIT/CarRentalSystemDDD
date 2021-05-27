namespace CarRentalSystem.Application.Features.CarAds.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Common;
    using Dealers;
    using CarRentalSystem.Domain.Common;
    using CarRentalSystem.Domain.Factories.CarAds;
    using CarRentalSystem.Domain.Models.CarAds;
    using MediatR;

    public class CreateCarAdCommand : CarAdCommand, IRequest<CreateCarAdOutputModel>
    {
        public class CreateCarAdCommandHandler : IRequestHandler<CreateCarAdCommand, CreateCarAdOutputModel>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IDealerRepository _dealerRepository;
            private readonly ICarAdRepository _carAdRepository;
            private readonly ICarAdFactory _carAdFactory;

            public CreateCarAdCommandHandler(
                ICurrentUser currentUser, 
                IDealerRepository dealerRepository, 
                ICarAdRepository carAdRepository, 
                ICarAdFactory carAdFactory)
            {
                _currentUser = currentUser;
                _dealerRepository = dealerRepository;
                _carAdRepository = carAdRepository;
                _carAdFactory = carAdFactory;
            }

            public async Task<CreateCarAdOutputModel> Handle(
                CreateCarAdCommand request, 
                CancellationToken cancellationToken)
            {
                var dealer = await _dealerRepository
                    .FindByUser(_currentUser.UserId!, cancellationToken);
                var category = await _carAdRepository
                    .GetCategory(request.Category, cancellationToken);
                var manufacturer = await _carAdRepository
                    .GetManufacturer(request.Manufacturer, cancellationToken);

                var factory = manufacturer == null
                    ? _carAdFactory.WithManufacturer(request.Manufacturer)
                    : _carAdFactory.WithManufacturer(manufacturer);

                var carAd = factory
                    .WithModel(request.Model)
                    .WithCategory(category)
                    .WithImageUrl(request.ImageUrl)
                    .WithPricePerDay(request.PricePerDay)
                    .WithOptions(
                        request.HasClimateControl,
                        request.NumberOfSeats,
                        Enumeration.FromValue<TransmissionType>(request.TransmissionType))
                    .Build();

                dealer.AddCarAd(carAd);

                await _carAdRepository.Save(carAd, cancellationToken);

                return new CreateCarAdOutputModel(carAd.Id);
            }
        }
    }
}