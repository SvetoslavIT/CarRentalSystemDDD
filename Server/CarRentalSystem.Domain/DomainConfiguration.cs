﻿namespace CarRentalSystem.Domain
{
    using Common;
    using Factories;
    using Models.CarAds;
    using Microsoft.Extensions.DependencyInjection;

    public static class DomainConfiguration
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
            => services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IFactory<>)))
                    .AsMatchingInterface()
                    .WithTransientLifetime())
                .AddTransient<IInitialData, CategoryData>();
    }
}
