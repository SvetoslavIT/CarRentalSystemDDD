namespace CarRentalSystem.Domain.Specifications.Dealers
{
    using System;
    using System.Linq.Expressions;
    using CarRentalSystem.Domain.Models.Dealers;

    public class DealerByIdSpecification : Specification<Dealer>
    {
        private readonly int? _dealerId;

        public DealerByIdSpecification(int? dealerId) => _dealerId = dealerId;

        protected override bool Include => _dealerId != null;

        public override Expression<Func<Dealer, bool>> ToExpression()
            => dealer => dealer.Id == _dealerId;
    }
}