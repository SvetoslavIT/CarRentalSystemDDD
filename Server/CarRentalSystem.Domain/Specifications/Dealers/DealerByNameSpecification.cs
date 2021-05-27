namespace CarRentalSystem.Domain.Specifications.Dealers
{
    using System;
    using System.Linq.Expressions;
    using CarRentalSystem.Domain.Models.Dealers;

    public class DealerByNameSpecification : Specification<Dealer>
    {
        private readonly string? _dealerName;

        public DealerByNameSpecification(string? dealerName) => _dealerName = dealerName;

        protected override bool Include => _dealerName != null;

        public override Expression<Func<Dealer, bool>> ToExpression()
            => dealer => dealer.Name.ToLower().Contains(_dealerName!.ToLower());
    }
}