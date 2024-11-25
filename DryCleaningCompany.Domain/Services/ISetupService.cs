namespace DryCleaningCompany.Domain.Services
{
    public interface ISetupService
    {
        Task DaysOpenSchedule(TimeOnly openingHour, TimeOnly closingHour);

        Task DaysOpen(DayOfWeek dayOfWeek, TimeOnly openingHour, TimeOnly closingHour);

        Task DatesOpen(DateOnly date, TimeOnly openingHour, TimeOnly closingHour);

        Task DaysClose(params DayOfWeek[] daysOfWeek);

        Task DatesClose(params DateOnly[] dates);
    }
}