namespace CarRentalSystem.Domain.Factories.Dealers
{
    using CarRentalSystem.Domain.Models.Dealers;

    internal class DealerFactory : IDealerFactory
    {
        private string _dealerName = default!;
        private string _dealerPhoneNumber = default!;

        public IDealerFactory WithName(string name)
        {
            _dealerName = name;
            return this;
        }

        public IDealerFactory WithPhoneNumber(string phoneNumber)
        {
            _dealerPhoneNumber = phoneNumber;
            return this;
        }

        public Dealer Build() => new(_dealerName, _dealerPhoneNumber);
    }
}