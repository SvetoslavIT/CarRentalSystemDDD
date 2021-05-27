namespace CarRentalSystem.Domain.Models.CarAds
{
    using FakeItEasy;
    using FluentAssertions;
    using Xunit;

    public class CarAdSpecs
    {
        [Fact]
        public void ChangeAvailabilityShouldMutateIsAvailable()
        {
            var carAd = A.Dummy<CarAd>();

            carAd.ChangeAvailability();

            carAd.IsAvailable.Should().BeFalse();
        }
    }
}
