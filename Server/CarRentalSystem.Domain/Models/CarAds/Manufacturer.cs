namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;
    using Exceptions;
    using static ModelConstants;

    public class Manufacturer : Entity<int>
    {
        internal Manufacturer(string name)
        {
            Validate(name);

            Name = name;
        }

        public string Name { get; }

        private static void Validate(string name)
            => Guard.ForStringLength<InvalidCarAdException>(
                name, 
                NameMinLength, 
                NameMaxLength, 
                nameof(Name));
    }
}