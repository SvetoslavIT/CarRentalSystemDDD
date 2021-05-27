namespace CarRentalSystem.Application.Features.CarAds
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using Queries.Categories;
    using Queries.Common;
    using Queries.Details;
    using CarRentalSystem.Domain.Models.CarAds;
    using CarRentalSystem.Domain.Models.Dealers;
    using Domain.Specifications;

    public interface ICarAdRepository : IRepository<CarAd>
    {
        Task<IEnumerable<TCarAdOutputModel>> GetCarAdListings<TCarAdOutputModel>(
            Specification<Dealer> dealerSpecification,
            Specification<CarAd> specification,
            CarAdSortOrder sortOrder,
            int skip,
            int take,
            CancellationToken cancellationToken = default);

        Task<int> Total(
            Specification<Dealer> dealerSpecification,
            Specification<CarAd> carAdSpecification, 
            CancellationToken cancellationToken = default);

        Task<Category> GetCategory(int category, CancellationToken cancellationToken = default);

        Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken = default);

        Task<IEnumerable<GetCarAdCategoryOutputModel>> GetCategories(CancellationToken cancellationToken = default);

        Task<CarAdDetailsOutputModel> Details(int id, CancellationToken cancellationToken = default);

        Task<CarAd> Find(int id, CancellationToken cancellationToken = default);

        Task Delete(int id, CancellationToken cancellationToken = default);
    }
}
