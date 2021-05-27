namespace CarRentalSystem.Application.Features.Dealers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Queries.Common;
    using Queries.Details;
    using CarRentalSystem.Domain.Models.Dealers;

    public interface IDealerRepository : IRepository<Dealer>
    {
        Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken = default);

        Task<int> GetDealerId(string userId, CancellationToken cancellationToken = default);

        Task<DealerDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default);

        Task<DealerOutputModel> GetDealer(int carAdId, CancellationToken cancellationToken = default);

        Task<bool> HasCarAd(int id, int carAdId, CancellationToken cancellationToken = default);
    }
}