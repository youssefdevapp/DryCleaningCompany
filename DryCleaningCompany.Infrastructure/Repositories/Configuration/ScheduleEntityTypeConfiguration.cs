using DryCleaningCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DryCleaningCompany.Infrastructure.Repositories.Configuration
{
    internal sealed class ScheduleEntityTypeConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(p => p.InitialDate);
            builder.Property(p => p.MinutesToAdd);
            builder.Property(p => p.FinalDate);
        }
    }
}