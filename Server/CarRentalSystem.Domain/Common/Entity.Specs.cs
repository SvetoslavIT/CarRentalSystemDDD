namespace CarRentalSystem.Domain.Common
{
    using Models.CarAds;
    using FluentAssertions;
    using Xunit;

    public class EntitySpecs
    {
        [Fact]
        public void EntitiesWithEqualIdShouldBeEqual()
        {
            var first = new Manufacturer("First").SetId(1);
            var second = new Manufacturer("Second").SetId(1);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Fact]
        public void EntitiesWithNotEqualIdShouldBeNotEqual()
        {
            var first = new Manufacturer("First").SetId(1);
            var second = new Manufacturer("Second").SetId(2);

            var result = first == second;

            result.Should().BeFalse();
        }
    }

    internal static class EntityExtensions
    {
        public static Entity<T> SetId<T>(this Entity<T> entity, int id)
            where T : struct
        {
            entity
                .GetType()
                .BaseType!
                .GetProperty(nameof(Entity<T>.Id))!
                .GetSetMethod(true)!
                .Invoke(entity, new object[] { id });

            return entity;
        }
    }
}
