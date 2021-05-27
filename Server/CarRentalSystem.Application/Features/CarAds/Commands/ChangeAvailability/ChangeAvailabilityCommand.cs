namespace CarRentalSystem.Application.Features.CarAds.Commands.ChangeAvailability
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Contracts;
    using Common;
    using Dealers;
    using MediatR;

    public class ChangeAvailabilityCommand : EntityCommand<int>, IRequest<Result>
    {
        public class ChangeAvailabilityCommandHandler 
            : IRequestHandler<ChangeAvailabilityCommand, Result>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IDealerRepository _dealerRepository;
            private readonly ICarAdRepository _carAdRepository;

            public ChangeAvailabilityCommandHandler(
                ICurrentUser currentUser, 
                IDealerRepository dealerRepository, 
                ICarAdRepository carAdRepository)
            {
                _currentUser = currentUser;
                _dealerRepository = dealerRepository;
                _carAdRepository = carAdRepository;
            }

            public async Task<Result> Handle(
                ChangeAvailabilityCommand request, 
                CancellationToken cancellationToken)
            {
                var result = await _currentUser
                    .DealerHasCarAd(_dealerRepository, request.Id, cancellationToken);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                var carAd = await _carAdRepository.Find(request.Id, cancellationToken);

                carAd.ChangeAvailability();

                await _carAdRepository.Save(carAd, cancellationToken);

                return result;
            }
        }
    }
}