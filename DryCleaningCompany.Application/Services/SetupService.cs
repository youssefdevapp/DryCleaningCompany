using DryCleaningCompany.Domain.Services;
using DryCleaningCompany.Infrastructure.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DryCleaningCompany.Application.Services
{
    public class SetupService : ISetupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DatesClose(params DateOnly[] dates)
        {
            foreach (var date in dates)
            {
                var schedule = new Domain.Entities.ShopSchedule();
                schedule.DateClose(date);
                await _unitOfWork.ShopSchedules.AddAsync(schedule);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task DatesOpen(DateOnly date, TimeOnly openingHour, TimeOnly closingHour)
        {
            var schedule = new Domain.Entities.ShopSchedule();
            schedule.DateOpen(date, openingHour, closingHour);
            await _unitOfWork.ShopSchedules.AddAsync(schedule);

            await _unitOfWork.SaveAsync();
        }

        public async Task DaysClose(params DayOfWeek[] daysOfWeek)
        {
            foreach (var day in daysOfWeek)
            {
                var schedule = new Domain.Entities.ShopSchedule();
                schedule.DayClose(day);
                await _unitOfWork.ShopSchedules.AddAsync(schedule);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task DaysOpen(DayOfWeek dayOfWeek, TimeOnly openingHour, TimeOnly closingHour)
        {
            var schedule = new Domain.Entities.ShopSchedule();
            schedule.DayOpen(dayOfWeek, openingHour, closingHour);
            await _unitOfWork.ShopSchedules.AddAsync(schedule);

            await _unitOfWork.SaveAsync();
        }

        public async Task DaysOpenSchedule(TimeOnly openingHour, TimeOnly closingHour)
        {
            var schedule = new Domain.Entities.ShopSchedule();
            schedule.DayOpenSchedule(openingHour, closingHour);
            await _unitOfWork.ShopSchedules.AddAsync(schedule);

            await _unitOfWork.SaveAsync();
        }
    }
}