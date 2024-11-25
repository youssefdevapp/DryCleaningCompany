using DryCleaningCompany.Domain.Entities;
using DryCleaningCompany.Domain.Services;
using DryCleaningCompany.Infrastructure.Repositories;

namespace DryCleaningCompany.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Schedule> CalculateNewSchedule(DateTime date, int minutes)
        {
            var shopSchedules = await _unitOfWork.ShopSchedules.GetAllAsync();

            var shopOpen = shopSchedules.Where(o => o.BusinessHourType == Domain.Core.BusinessHourType.Open);
            var shopClose = shopSchedules.Where(o => o.BusinessHourType == Domain.Core.BusinessHourType.Close);

            var schedule = new Schedule(date, minutes);

            var openingHour = shopOpen.First(o => o.OpeningHour != null).OpeningHour.Value.ToTimeSpan();

            DateTime finalDate = schedule.FinalDate.Add(openingHour);

            while (shopClose.Any(d => d.Day == finalDate.DayOfWeek)
                || shopClose.Any(d => d.Date == DateOnly.FromDateTime(finalDate.Date))
                || finalDate.TimeOfDay < shopOpen.First(o => o.OpeningHour != null).OpeningHour.Value.ToTimeSpan()
                || finalDate.TimeOfDay >= shopOpen.First(o => o.ClosingHour != null).ClosingHour.Value.ToTimeSpan())
            {
                finalDate = finalDate.AddDays(1).Date.Add(openingHour);
            }

            schedule.FinalDate = finalDate;

            await _unitOfWork.Schedules.AddAsync(schedule);
            await _unitOfWork.SaveAsync();

            return schedule;
        }
    }
}