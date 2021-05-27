namespace CarRentalSystem.Application.Features.CarAds.Queries.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using CarRentalSystem.Domain.Models.CarAds;
    using CarRentalSystem.Domain.Models.Dealers;
    using Domain.Specifications;
    using CarRentalSystem.Domain.Specifications.CarAds;
    using CarRentalSystem.Domain.Specifications.Dealers;

    public abstract class CarAdQuery
    {
        public const int CarAdsPerPage = 10;

        public string? Manufacturer { get; set; }

        public string? Dealer { get; set; }

        public int? Category { get; set; }

        public decimal? MinPricePerDay { get; set; }

        public decimal? MaxPricePerDay { get; set; }

        public string? SortBy { get; set; }

        public string? Order { get; set; }

        public int Page { get; set; } = 1;

        public abstract class CarAdQueryHandler
        {
            private readonly ICarAdRepository _carAdRepository;

            protected CarAdQueryHandler(ICarAdRepository carAdRepository)
                => _carAdRepository = carAdRepository;

            public async Task<IEnumerable<TCarAdsOutputModel>> GetCarAdListings<TCarAdsOutputModel>(
                CarAdQuery request, 
                int? dealerId = default,
                bool onlyAvailable = true,
                CancellationToken cancellationToken = default)
            {
                var dealerSpecification = GetDealerSpecification(request, dealerId);
                var carAdSpecification = GetCarAdSpecification(request, onlyAvailable);

                var skip = (request.Page - 1) * CarAdsPerPage;
                var sortOrder = new CarAdSortOrder(request.SortBy, request.Order);

                return await _carAdRepository.GetCarAdListings<TCarAdsOutputModel>(
                    dealerSpecification,
                    carAdSpecification,
                    sortOrder,
                    skip,
                    CarAdsPerPage,
                    cancellationToken);
            }

            public async Task<int> GetTotalPages(
                CarAdQuery request,
                int? dealerId = default,
                bool onlyAvailable = true,
                CancellationToken cancellationToken = default)
            {
                var dealerSpecification = GetDealerSpecification(request, dealerId);
                var carAdSpecification = GetCarAdSpecification(request, onlyAvailable);

                var totalCarAds = await _carAdRepository.Total(
                    dealerSpecification,
                    carAdSpecification,
                    cancellationToken);

                return (int)Math.Ceiling((double)totalCarAds / CarAdsPerPage);
            }
            
            private static Specification<CarAd> GetCarAdSpecification(CarAdQuery request, bool onlyAvailable) 
                => new CarAdByManufacturerSpecification(request.Manufacturer)
                    .And(new CarAdByCategorySpecification(request.Category))
                    .And(new CarAdByPricePerDaySpecification(request.MinPricePerDay, request.MaxPricePerDay))
                    .And(new CarAdOnlyAvailableSpecification(onlyAvailable));

            private static Specification<Dealer> GetDealerSpecification(CarAdQuery request, int? dealerId) 
                => new DealerByIdSpecification(dealerId)
                    .And(new DealerByNameSpecification(request.Dealer));
        }
    }
}