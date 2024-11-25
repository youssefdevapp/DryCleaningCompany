using DryCleaningCompany.Application.Services;
using DryCleaningCompany.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DryCleaningCompany.Application
{
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ISetupService, SetupService>();

            return services;
        }
    }
}