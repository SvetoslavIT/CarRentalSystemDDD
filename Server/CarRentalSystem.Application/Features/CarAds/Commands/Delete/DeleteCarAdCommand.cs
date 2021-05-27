namespace CarRentalSystem.Application.Features.CarAds.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using Contracts;
    using Common;
    using Dealers;
    using MediatR;

    public class DeleteCarAdCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeleteCarAdCommandHandler : IRequestHandler<DeleteCarAdCommand, Result>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IDealerRepository _dealerRepository;
            private readonly ICarAdRepository _carAdRepository;

            public DeleteCarAdCommandHandler(
                ICurrentUser currentUser, 
                IDealerRepository dealerRepository, 
                ICarAdRepository carAdRepository)
            {
                _currentUser = currentUser;
                _dealerRepository = dealerRepository;
                _carAdRepository = carAdRepository;
            }

            public async Task<Result> Handle(DeleteCarAdCommand request, CancellationToken cancellationToken)
            {
                var result = await _currentUser
                    .DealerHasCarAd(_dealerRepository, request.Id, cancellationToken);
                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                await _carAdRepository.Delete(request.Id, cancellationToken);

                return result;
            }
        }
    }
}