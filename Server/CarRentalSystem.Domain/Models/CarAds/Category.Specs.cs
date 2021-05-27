namespace CarRentalSystem.Domain.Models.CarAds
{
    using System;
    using Exceptions;
    using FluentAssertions;
    using Xunit;

    public class CategorySpecs
    {
        [Fact]
        public void ValidateCategoryNotThrowException()
        {
            Action act = () => new Category("Valid name", "Valid description text");

            act.Should().NotThrow<InvalidCarAdException>();
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            Action act = () => new Category("", "Valid description text");

            act.Should().Throw<InvalidCarAdException>();
        }
    }
}
