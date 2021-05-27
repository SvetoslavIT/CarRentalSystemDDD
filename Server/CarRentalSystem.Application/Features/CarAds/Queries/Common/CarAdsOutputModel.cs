namespace CarRentalSystem.Application.Features.CarAds.Queries.Common
{
    using System.Collections.Generic;

    public abstract class CarAdsOutputModel<TCarAdOutputModel>
    {
        protected CarAdsOutputModel(IEnumerable<TCarAdOutputModel> carAds, int currentPage, int totalPages)
        {
            CarAds = carAds;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }

        public IEnumerable<TCarAdOutputModel> CarAds { get; }

        public int CurrentPage { get; }

        public int TotalPages { get; }
    }
}