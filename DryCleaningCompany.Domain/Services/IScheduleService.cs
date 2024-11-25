using DryCleaningCompany.Domain.Entities;

namespace DryCleaningCompany.Domain.Services
{
    public interface IScheduleService
    {
        Task<Schedule> CalculateNewSchedule(DateTime date, int minutes);
    }
}