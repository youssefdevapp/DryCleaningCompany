using DryCleaningCompany.API.Dtos;
using DryCleaningCompany.Domain.Services;
using DryCleaningCompany.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace DryCleaningCompany.API
{
    public static class ApiConfiguration
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            if (app is not null)
            {
                using var scope = app.ApplicationServices.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DryCleaningDbContext>();
                db.InitializeData();
            }
        }

        public static void AddFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<ScheduleRequest>();
        }
    }
}