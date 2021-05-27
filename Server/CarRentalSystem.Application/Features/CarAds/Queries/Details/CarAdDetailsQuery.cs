namespace CarRentalSystem.Application.Features.CarAds.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using Dealers;
    using CarRentalSystem.Domain.Models.CarAds;
    using MediatR;

    public class CarAdDetailsQuery : EntityCommand<int>, IRequest<CarAdDetailsOutputModel>
    {
        public class CarAdDetailsQueryHandler : IRequestHandler<CarAdDetailsQuery, CarAdDetailsOutputModel>
        {
            private readonly ICarAdRepository _carAdRepository;
            private readonly IDealerRepository _dealerRepository;

            public CarAdDetailsQueryHandler(
                ICarAdRepository carAdRepository, 
                IDealerRepository dealerRepository)
            {
                _carAdRepository = carAdRepository;
                _dealerRepository = dealerRepository;
            }

            public async Task<CarAdDetailsOutputModel> Handle(
                CarAdDetailsQuery request, 
                CancellationToken cancellationToken)
            {
                var details = await _carAdRepository
                    .Details(request.Id, cancellationToken);

                if (details == null)
                {
                    throw new NotFoundException(nameof(CarAd), request.Id);
                }

                details.Dealer = await _dealerRepository
                    .GetDealer(request.Id, cancellationToken);

                return details;
            }
        }
    }
}