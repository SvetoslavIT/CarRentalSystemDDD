namespace CarRentalSystem.Application.Features.CarAds.Queries.Common
{
    using System;
    using System.Linq.Expressions;
    using Application.Common;
    using CarRentalSystem.Domain.Models.CarAds;

    public class CarAdSortOrder : SortOrder<CarAd>
    {
        public CarAdSortOrder(string? sortBy, string? order) 
            : base(sortBy, order)
        {
        }

        public override Expression<Func<CarAd, object>> ToExpression()
            => SortBy switch
            {
                "price" => carAd => carAd.PricePerDay,
                "manufacturer" => carAd => carAd.Manufacturer.Name,
                _ => carAd => carAd.Id
            };
    }
}