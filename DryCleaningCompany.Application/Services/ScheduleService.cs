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

            var openingHourDate = shopOpen
                .FirstOrDefault(o => o.OpeningHour != null && o.Date == DateOnly.FromDateTime(schedule.FinalDate.Date));

            openingHourDate ??= shopOpen
                    .FirstOrDefault(o => o.OpeningHour != null && o.Day == schedule.FinalDate.DayOfWeek);

            openingHourDate ??= shopOpen.First(o => o.OpeningHour != null
                && o.Date == null
                && o.Day == null);

            var openingHour = openingHourDate.OpeningHour.Value.ToTimeSpan();

            if (date.TimeOfDay < openingHour)
            {
                schedule.FinalDate = date.Date.Add(openingHour).AddMinutes(minutes);
            }

            var closeingHourDate = shopOpen
                .FirstOrDefault(o => o.ClosingHour != null && o.Date == DateOnly.FromDateTime(schedule.FinalDate.Date));

            closeingHourDate ??= shopOpen.FirstOrDefault(o => o.ClosingHour != null && o.Day == schedule.FinalDate.DayOfWeek);

            closeingHourDate ??= shopOpen.First(o => o.ClosingHour != null
                && o.Date == null
                && o.Day == null);

            var closeingHour = closeingHourDate.ClosingHour.Value.ToTimeSpan();

            int availableMinutes = (int)(closeingHour - schedule.FinalDate.TimeOfDay).TotalMinutes;

            if (minutes <= availableMinutes)
            {
                minutes = 0;
            }
            else
            {
                minutes = availableMinutes * -1;
            }

            while (shopClose.Any(d => d.Day == schedule.FinalDate.DayOfWeek)
                || shopClose.Any(d => d.Date == DateOnly.FromDateTime(schedule.FinalDate.Date))
                || schedule.FinalDate.TimeOfDay < openingHour
                || schedule.FinalDate.TimeOfDay >= closeingHour)
            {
                schedule.FinalDate = schedule.FinalDate.AddDays(1).Date.Add(openingHour);

                openingHourDate = shopOpen
                    .FirstOrDefault(o => o.OpeningHour != null && o.Date == DateOnly.FromDateTime(schedule.FinalDate.Date));

                openingHourDate ??= shopOpen
                        .FirstOrDefault(o => o.OpeningHour != null && o.Day == schedule.FinalDate.DayOfWeek);

                openingHourDate ??= shopOpen.First(o => o.OpeningHour != null
                    && o.Date == null
                    && o.Day == null);

                openingHour = openingHourDate.OpeningHour.Value.ToTimeSpan();
            }

            schedule.FinalDate = schedule.FinalDate.AddMinutes(minutes);

            await _unitOfWork.Schedules.AddAsync(schedule);
            await _unitOfWork.SaveAsync();

            return schedule;
        }
    }
}