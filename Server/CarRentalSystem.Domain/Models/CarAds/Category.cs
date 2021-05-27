namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;
    using static ModelConstants;

    public class Category : Entity<int>
    {
        internal Category(string name, string description)
        {
            Validate(name, description);

            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }

        private static void Validate(string name, string description)
        {
            Guard.ForStringLength<InvalidCarAdException>(
                name, 
                NameMinLength, 
                NameMaxLength, 
                nameof(Name));

            Guard.ForStringLength<InvalidCarAdException>(
                description,
                DescriptionMinLength,
                DescriptionMaxLength,
                nameof(Description));
        }
    }
}