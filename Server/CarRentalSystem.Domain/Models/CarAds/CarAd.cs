namespace CarRentalSystem.Domain.Models.CarAds
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Exceptions;
    using static ModelConstants;

    public class CarAd : Entity<int>, IAggregateRoot
    {
        private static readonly IEnumerable<Category> AllowedCategory
            = new CategoryData().GetData().Cast<Category>();

        public CarAd(
            Manufacturer manufacturer, 
            string model, 
            Category category, 
            string imageUrl, 
            decimal pricePerDay, 
            Options options, 
            bool isAvailable)
        {
            Validate(model, imageUrl, pricePerDay);
            ValidateCategory(category);

            Manufacturer = manufacturer;
            Model = model;
            Category = category;
            ImageUrl = imageUrl;
            PricePerDay = pricePerDay;
            Options = options;
            IsAvailable = isAvailable;
        }

        private CarAd(
            string model,
            string imageUrl,
            decimal pricePerDay,
            bool isAvailable)
        {
            Model = model;
            ImageUrl = imageUrl;
            PricePerDay = pricePerDay;
            IsAvailable = isAvailable;

            Manufacturer = default!;
            Category = default!;
            Options = default!;
        }

        public Manufacturer Manufacturer { get; private set; }

        public string Model { get; private set; }

        public Category Category { get; private set; }

        public string ImageUrl { get; private set; }

        public decimal PricePerDay { get; private set; }

        public Options Options { get; private set; }

        public bool IsAvailable { get; private set; }

        public CarAd UpdateManufacturer(string manufacturer)
        {
            Manufacturer = new Manufacturer(manufacturer);
            return this;
        }

        public CarAd UpdateManufacturer(Manufacturer manufacturer)
        {
            Manufacturer = manufacturer;
            return this;
        }

        public CarAd UpdateModel(string model)
        {
            ValidateModel(model);

            Model = model;
            return this;
        }

        public CarAd UpdateCategory(Category category)
        {
            Category = category;
            return this;
        }

        public CarAd UpdateImageUrl(string imageUrl)
        {
            ValidateImageUrl(imageUrl);

            ImageUrl = imageUrl;
            return this;
        }

        public CarAd UpdatePricePerDay(decimal pricePerDay)
        {
            ValidatePricePerDay(pricePerDay);

            PricePerDay = pricePerDay;
            return this;
        }

        public CarAd UpdateOptions(
            bool hasClimateControl, 
            int numberOfSeats, 
            TransmissionType transmissionType)
        {
            Options = new Options(hasClimateControl, numberOfSeats, transmissionType);
            return this;
        }

        public CarAd ChangeAvailability()
        {
            IsAvailable = !IsAvailable;
            return this;
        }

        private static void Validate(string model, string imageUrl, decimal pricePerDay)
        {
            ValidateModel(model);
            ValidateImageUrl(imageUrl);
            ValidatePricePerDay(pricePerDay);
        }

        private static void ValidateModel(string model) 
            => Guard.ForStringLength<InvalidCarAdException>(
                model,
                ModelMinLength,
                ModelMaxLength,
                nameof(Model));

        private static void ValidateImageUrl(string imageUrl) 
            => Guard.ForValidUrl<InvalidCarAdException>(
                imageUrl,
                nameof(ImageUrl));

        private static void ValidatePricePerDay(decimal pricePerDay) 
            => Guard.AgainstOutOfRange<InvalidCarAdException>(
                pricePerDay,
                MinPricePerDay,
                MaxPricePerDay,
                nameof(PricePerDay));


        private static void ValidateCategory(Category category)
        {
            var categoryName = category.Name;

            if (AllowedCategory.Any(c => c.Name == categoryName))
            {
                return;
            }

            var allowedCategoryNames = string.Join(", ", AllowedCategory.Select(c => $"'{c.Name}'"));

            throw new InvalidCarAdException(
                $"'{categoryName}' is not a valid category. Allowed values are: {allowedCategoryNames}");
        }
    }
}