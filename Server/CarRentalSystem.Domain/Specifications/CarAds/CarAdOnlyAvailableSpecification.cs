namespace CarRentalSystem.Domain.Specifications.CarAds
{
    using System;
    using System.Linq.Expressions;
    using CarRentalSystem.Domain.Models.CarAds;

    public class CarAdOnlyAvailableSpecification : Specification<CarAd>
    {
        private readonly bool _onlyAvailable;

        public CarAdOnlyAvailableSpecification(bool onlyAvailable)
            => _onlyAvailable = onlyAvailable;

        public override Expression<Func<CarAd, bool>> ToExpression()
        {
            if (_onlyAvailable)
            {
                return carAd => carAd.IsAvailable;
            }

            return carAd => true;
        }
    }
}