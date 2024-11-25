using DryCleaningCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DryCleaningCompany.Infrastructure.Data
{
    public class DryCleaningDbContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ShopSchedule> ShopSchedules { get; set; }

        public DryCleaningDbContext(DbContextOptions<DryCleaningDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DryCleaningDbContext).Assembly);
        }

        /// <summary>
        /// Initializes the data in the context.
        /// </summary>
        public void InitializeData()
        {
            if (!ShopSchedules.Any())
            {
                ShopSchedules.Add(
                    new ShopSchedule()
                    {
                        BusinessHourType = Domain.Core.BusinessHourType.Open,
                        OpeningHour = new TimeOnly(9, 0),
                        ClosingHour = new TimeOnly(15, 0),
                    });

                ShopSchedules.Add(
                    new ShopSchedule()
                    {
                        BusinessHourType = Domain.Core.BusinessHourType.Open,
                        Day = DayOfWeek.Friday,
                        OpeningHour = new TimeOnly(10, 0),
                        ClosingHour = new TimeOnly(17, 0),
                    });

                ShopSchedules.Add(
                    new ShopSchedule()
                    {
                        BusinessHourType = Domain.Core.BusinessHourType.Open,
                        Date = new DateOnly(2010, 12, 24),
                        OpeningHour = new TimeOnly(8, 0),
                        ClosingHour = new TimeOnly(13, 0),
                    });

                ShopSchedules.Add(
                    new ShopSchedule()
                    {
                        BusinessHourType = Domain.Core.BusinessHourType.Close,
                        Day = DayOfWeek.Sunday
                    });

                ShopSchedules.Add(
                    new ShopSchedule()
                    {
                        BusinessHourType = Domain.Core.BusinessHourType.Close,
                        Day = DayOfWeek.Wednesday
                    });

                ShopSchedules.Add(
                    new ShopSchedule()
                    {
                        BusinessHourType = Domain.Core.BusinessHourType.Close,
                        Date = new DateOnly(2010, 12, 25)
                    });
            }

            SaveChanges();
        }
    }
}