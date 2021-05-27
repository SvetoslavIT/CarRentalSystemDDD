namespace CarRentalSystem.Domain.Specifications.CarAds
{
    using System;
    using System.Linq.Expressions;
    using CarRentalSystem.Domain.Models.CarAds;

    public class CarAdByCategorySpecification : Specification<CarAd>
    {
        private readonly int? _category;

        public CarAdByCategorySpecification(int? category) => _category = category;

        protected override bool Include => _category != null;

        public override Expression<Func<CarAd, bool>> ToExpression()
            => carAd => carAd.Category.Id == _category;
    }
}