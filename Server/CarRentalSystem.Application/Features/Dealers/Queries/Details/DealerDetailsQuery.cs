namespace CarRentalSystem.Application.Features.Dealers.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public class DealerDetailsQuery : EntityCommand<int>, IRequest<DealerDetailsOutputModel>
    {
        public class DealerDetailsQueryHandler : IRequestHandler<DealerDetailsQuery, DealerDetailsOutputModel>
        {
            private readonly IDealerRepository _dealerRepository;

            public DealerDetailsQueryHandler(IDealerRepository dealerRepository) 
                => _dealerRepository = dealerRepository;

            public async Task<DealerDetailsOutputModel> Handle(
                DealerDetailsQuery request,
                CancellationToken cancellationToken)
                => await _dealerRepository.Details(request.Id, cancellationToken);
        }
    }
}