namespace CarRentalSystem.Startup
{
    using Application;
    using Domain;
    using Infrastructure;
    using Web;
    using Web.Middleware;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
            => services
                .AddDomain()
                .AddApplication(Configuration)
                .AddInfrastructure(Configuration)
                .AddWebComponents()
                .AddSwagger();

        public void Configure(IApplicationBuilder app)
            => app
                .UseValidationExceptionHandler()
                .UseDefaultSwagger()
                .UseHttpsRedirection()
                .UseRouting()
                .UseCors(cfg => cfg
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .Initialize();
    }
}
