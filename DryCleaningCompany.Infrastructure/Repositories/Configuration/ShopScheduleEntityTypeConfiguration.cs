using DryCleaningCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleaningCompany.Infrastructure.Repositories.Configuration
{
    public class ShopScheduleEntityTypeConfiguration : IEntityTypeConfiguration<ShopSchedule>
    {
        public void Configure(EntityTypeBuilder<ShopSchedule> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(p => p.OpeningHour);
            builder.Property(p => p.ClosingHour);
            builder.Property(p => p.BusinessHourType);
        }
    }
}