namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Application.Features.CarAds;
    using Application.Features.CarAds.Queries.Categories;
    using CarRentalSystem.Application.Features.CarAds.Queries.Common;
    using Application.Features.CarAds.Queries.Details;
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Domain.Specifications;
    using Common;
    using Microsoft.EntityFrameworkCore;

    internal class CarAdRepository : DataRepository<CarAd>, ICarAdRepository
    {
        private readonly IMapper _mapper;

        public CarAdRepository(CarRentalDbContext db, IMapper mapper)
            : base(db) 
            => _mapper = mapper;

        public async Task<IEnumerable<TCarAdOutputModel>> GetCarAdListings<TCarAdOutputModel>(
            Specification<Dealer> dealerSpecification,
            Specification<CarAd> carAdSpecification,
            CarAdSortOrder sortOrder,
            int skip,
            int take,
            CancellationToken cancellationToken = default)
            => await _mapper
                .ProjectTo<TCarAdOutputModel>(
                    AllFiltered(dealerSpecification, carAdSpecification)
                        .Sort(sortOrder)
                        .Skip(skip)
                        .Take(take))
                .ToListAsync(cancellationToken);

        public async Task<int> Total(
            Specification<Dealer> dealerSpecification,
            Specification<CarAd> carAdSpecification,
            CancellationToken cancellationToken = default)
            => await AllFiltered(dealerSpecification, carAdSpecification)
                .CountAsync(cancellationToken);

        public Task<Category> GetCategory(int category, CancellationToken cancellationToken = default)
            => Data
                .Categories
                .FirstOrDefaultAsync(c => c.Id == category, cancellationToken);

        public Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken = default)
            => Data
                .Manufacturers
                .FirstOrDefaultAsync(c => c.Name == manufacturer, cancellationToken);

        public async Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCategories(CancellationToken cancellationToken = default)
        {
            var categories = await _mapper
                .ProjectTo<GetCarAdCategoryOutputModel>(Data.Categories)
                .ToDictionaryAsync(c => c.Id, cancellationToken);

            var carAds = await AllAvailable()
                .GroupBy(c => c.Category.Id)
                .Select(c => new
                {
                    Id = c.Key,
                    Count = c.Count()
                })
                .ToListAsync(cancellationToken);

            carAds.ForEach(c => categories[c.Id].TotalCarAds = c.Count);

            return categories.Values;
        }

        public Task<CarAdDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default)
            => _mapper
                .ProjectTo<CarAdDetailsOutputModel>(AllAvailable().Where(c => c.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public Task<CarAd> Find(int id, CancellationToken cancellationToken = default)
            => All()
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            var carAd = await Data.CarAds.FindAsync(id);

            Data.CarAds.Remove(carAd);
            await Data.SaveChangesAsync(cancellationToken);
        }

        private IQueryable<CarAd> AllAvailable()
            => All()
                .Where(car => car.IsAvailable);

        private IQueryable<CarAd> AllFiltered(
            Specification<Dealer> dealerSpecification,
            Specification<CarAd> carAdSpecification) 
            => Data
                .Dealers
                .Where(dealerSpecification)
                .SelectMany(d => d.CarAds)
                .Where(carAdSpecification);
    }
}
