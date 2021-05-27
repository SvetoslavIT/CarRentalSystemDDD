namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Application.Features.Dealers;
    using CarRentalSystem.Application.Features.Dealers.Queries.Common;
    using Application.Features.Dealers.Queries.Details;
    using Domain.Exceptions;
    using Domain.Models.Dealers;
    using Identity;
    using Microsoft.EntityFrameworkCore;

    internal class DealerRepository : DataRepository<Dealer>, IDealerRepository
    {
        private readonly IMapper _mapper;

        public DealerRepository(CarRentalDbContext db, IMapper mapper) 
            : base(db) 
            => _mapper = mapper;

        public Task<Dealer> FindByUser(string userId, CancellationToken cancellationToken = default)
            => FindByUser(userId, user => user.Dealer!, cancellationToken);

        public Task<int> GetDealerId(string userId, CancellationToken cancellationToken = default)
            => FindByUser(userId, user => user.Dealer!.Id, cancellationToken);

        public Task<DealerDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default)
            => _mapper
                .ProjectTo<DealerDetailsOutputModel>(All().Where(d => d.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public Task<DealerOutputModel> GetDealer(int carAdId, CancellationToken cancellationToken = default)
            => _mapper
                .ProjectTo<DealerOutputModel>(All().Where(d => d.CarAds.Any(c => c.Id == carAdId)))
                .FirstOrDefaultAsync(cancellationToken);

        public Task<bool> HasCarAd(int id, int carAdId, CancellationToken cancellationToken = default)
            => All()
                .Where(d => d.Id == id)
                .AnyAsync(d => d.CarAds.Any(c => c.Id == carAdId), cancellationToken);

        private async Task<TResult> FindByUser<TResult>(
            string userId, 
            Expression<Func<User, TResult>> selector, 
            CancellationToken cancellationToken = default)
        {

            var dealer = await Data.Users
                .Where(u => u.Id == userId)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);

            if (dealer == null)
            {
                throw new InvalidDealerException("This user is not a dealer.");
            }

            return dealer;
        }
    }
}