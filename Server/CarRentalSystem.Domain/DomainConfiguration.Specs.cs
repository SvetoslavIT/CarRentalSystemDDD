﻿namespace CarRentalSystem.Domain
{
    using Factories.CarAds;
    using Factories.Dealers;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DomainConfigurationSpecs
    {
        [Fact]
        public void AddDomainShouldRegisterFactories()
        {
            var serviceCollection = new ServiceCollection();
            
            var services = serviceCollection
                .AddDomain()
                .BuildServiceProvider();
            
            services
                .GetService<IDealerFactory>()
                .Should()
                .NotBeNull();

            services
                .GetService<ICarAdFactory>()
                .Should()
                .NotBeNull();
        }
    }
}