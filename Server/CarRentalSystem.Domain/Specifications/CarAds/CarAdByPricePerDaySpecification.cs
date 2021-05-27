namespace CarRentalSystem.Domain.Specifications.CarAds
{
    using System;
    using System.Linq.Expressions;
    using CarRentalSystem.Domain.Models.CarAds;
    using static Models.ModelConstants;

    public class CarAdByPricePerDaySpecification : Specification<CarAd>
    {
        private readonly decimal _minPrice;
        private readonly decimal _maxPrice;

        public CarAdByPricePerDaySpecification(
            decimal? minPrice = default, 
            decimal? maxPrice = MaxPricePerDay)
        {
            _minPrice = minPrice ?? default;
            _maxPrice = maxPrice ?? MaxPricePerDay;
        }

        public override Expression<Func<CarAd, bool>> ToExpression()
            => carAd => _minPrice < carAd.PricePerDay && carAd.PricePerDay < _maxPrice;
    }
}