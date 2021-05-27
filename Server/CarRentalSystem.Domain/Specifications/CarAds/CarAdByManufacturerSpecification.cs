namespace CarRentalSystem.Domain.Specifications.CarAds
{
    using System;
    using System.Linq.Expressions;
    using CarRentalSystem.Domain.Models.CarAds;

    public class CarAdByManufacturerSpecification : Specification<CarAd>
    {
        private readonly string? _manufacturer;

        public CarAdByManufacturerSpecification(string? manufacturer) => _manufacturer = manufacturer;

        protected override bool Include => _manufacturer != null;

        public override Expression<Func<CarAd, bool>> ToExpression()
            => carAd => carAd.Manufacturer.Name.ToLower()
                .Contains(_manufacturer!.ToLower());
    }
}