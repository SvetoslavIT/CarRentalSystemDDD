namespace CarRentalSystem.Domain.Models
{
    public class ModelConstants
    {
        public const int UrlMaxLength = 2048;

        public const int NameMinLength = 3;
        public const int NameMaxLength = 20;

        public const int DescriptionMinLength = 20;
        public const int DescriptionMaxLength = 1000;

        public const int ModelMinLength = 3;
        public const int ModelMaxLength = 20;

        public const int MinPricePerDay = 0;
        public const decimal MaxPricePerDay = 9999999999999999.99m;

        public const int MinNumberOfSeats = 2;
        public const int MaxNumberOfSeats = 50;

        public const int EmailMinLength = 5;
        public const int EmailMaxLength = 100;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 20;

        public const int PhoneNumberMinLength = 6;
        public const int PhoneNumberMaxLength = 18;
        public const string PhoneNumberPattern = @"^\+[0-9]+$";
    }
}