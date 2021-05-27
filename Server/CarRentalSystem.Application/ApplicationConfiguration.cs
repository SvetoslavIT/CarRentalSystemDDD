namespace CarRentalSystem.Application
{
    using System;
    using System.Reflection;
    using Behaviours;
    using Services;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)),
                    options => options.BindNonPublicProperties = true)
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        public static IServiceCollection AddConventionalServices(
            this IServiceCollection services, 
            Type type)
        {
            services
                .Scan(scan => scan
                    .FromAssembliesOf(type)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(ISingletonService)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());

            services
                .Scan(scan => scan
                    .FromAssembliesOf(type)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IScopedService)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services
                .Scan(scan => scan
                    .FromAssembliesOf(type)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IService)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            
            return services;
        }
    }
}