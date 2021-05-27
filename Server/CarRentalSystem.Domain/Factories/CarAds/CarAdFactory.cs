namespace CarRentalSystem.Domain.Factories.CarAds
{
    using Exceptions;
    using CarRentalSystem.Domain.Models.CarAds;

    internal class CarAdFactory : ICarAdFactory
    {
        private Manufacturer _carAdManufacturer = default!;
        private string _carAdModel = default!;
        private Category _carAdCategory = default!;
        private string _carAdImageUrl = default!;
        private decimal _carAdPricePerDay;
        private Options _carAdOptions = default!;

        private bool _hasManufacturer;
        private bool _hasCategory;
        private bool _hasOptions;

        public ICarAdFactory WithManufacturer(string name)
            => WithManufacturer(new Manufacturer(name));

        public ICarAdFactory WithManufacturer(Manufacturer manufacturer)
        {
            _carAdManufacturer = manufacturer;
            _hasManufacturer = true;
            return this;
        }

        public ICarAdFactory WithModel(string model)
        {
            _carAdModel = model;
            return this;
        }
        
        public ICarAdFactory WithCategory(Category category)
        {
            _carAdCategory = category;
            _hasCategory = true;
            return this;
        }

        public ICarAdFactory WithImageUrl(string imageUrl)
        {
            _carAdImageUrl = imageUrl;
            return this;
        }

        public ICarAdFactory WithPricePerDay(decimal pricePerDay)
        {
            _carAdPricePerDay = pricePerDay;
            return this;
        }

        public ICarAdFactory WithOptions(
            bool hasClimateControl,
            int numberOfSeats,
            TransmissionType transmissionType)
            => WithOptions(new Options(hasClimateControl, numberOfSeats, transmissionType));

        public ICarAdFactory WithOptions(Options options)
        {
            _carAdOptions = options;
            _hasOptions = true;
            return this;
        }

        public CarAd Build()
        {
            if (!_hasOptions || !_hasCategory || !_hasManufacturer)
            {
                throw new InvalidCarAdException("Can not create 'CarAd' without manufacturer, category or options.");
            }

            return new CarAd(
                _carAdManufacturer, 
                _carAdModel, 
                _carAdCategory, 
                _carAdImageUrl, 
                _carAdPricePerDay,
                _carAdOptions, 
                true);
        }
    }
}