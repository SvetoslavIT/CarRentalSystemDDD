namespace CarRentalSystem.Application.Features.CarAds.Queries.Mine
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Common;
    using Dealers;
    using MediatR;

    public class MineCarAdQuery : CarAdQuery, IRequest<MineCarAdsOutputModel>
    {
        public class MineCarAdQueryHandler : CarAdQueryHandler,
            IRequestHandler<MineCarAdQuery, MineCarAdsOutputModel>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IDealerRepository _dealerRepository;

            public MineCarAdQueryHandler(
                ICarAdRepository carAdRepository, 
                ICurrentUser currentUser, 
                IDealerRepository dealerRepository) 
                : base(carAdRepository)
            {
                _currentUser = currentUser;
                _dealerRepository = dealerRepository;
            }

            public async Task<MineCarAdsOutputModel> Handle(
                MineCarAdQuery request, 
                CancellationToken cancellationToken)
            {
                var dealerId = await _dealerRepository
                    .GetDealerId(_currentUser.UserId!, cancellationToken);

                var carAds = await GetCarAdListings<MineCarAdOutputModel>(
                    request, 
                    dealerId, 
                    false, 
                    cancellationToken);
                var totalPages = await GetTotalPages(
                    request, 
                    dealerId, 
                    false, 
                    cancellationToken);

                return new MineCarAdsOutputModel(carAds, request.Page, totalPages);
            }
        }
    }
}