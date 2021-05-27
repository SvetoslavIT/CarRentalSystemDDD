namespace CarRentalSystem.Domain.Models.Dealers
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Exceptions;
    using CarAds;
    using static ModelConstants;

    public class Dealer : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<CarAd> _carAds = new();

        public Dealer(string name, string phoneNumber)
        {
            Validate(name);

            Name = name;
            PhoneNumber = phoneNumber;
        }

        private Dealer(string name)
        {
            Name = name;
            
            PhoneNumber = default!;
        }

        public string Name { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public IReadOnlyCollection<CarAd> CarAds => _carAds.ToList().AsReadOnly();

        public void AddCarAd(CarAd carAd) => _carAds.Add(carAd);

        public Dealer UpdateName(string name)
        {
            Validate(name);

            Name = name;
            return this;
        }

        public Dealer UpdatePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        private static void Validate(string name)
            => Guard.ForStringLength<InvalidDealerException>(
                name, 
                NameMinLength, 
                NameMaxLength, 
                nameof(Name));
    }
}