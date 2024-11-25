using DryCleaningCompany.Domain.Core;

namespace DryCleaningCompany.Domain.Entities
{
    public class ShopSchedule : EntityBase
    {
        public DayOfWeek? Day { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? OpeningHour { get; set; }
        public TimeOnly? ClosingHour { get; set; }
        public BusinessHourType BusinessHourType { get; set; }

        public void DateClose(DateOnly date)
        {
            BusinessHourType = Domain.Core.BusinessHourType.Open;
            Date = date;
            OpeningHour = null;
            ClosingHour = null;
        }

        public void DateOpen(DateOnly date, TimeOnly openingHour, TimeOnly closingHour)
        {
            BusinessHourType = Domain.Core.BusinessHourType.Open;
            Date = date;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
        }

        public void DayClose(DayOfWeek dayOfWeek)
        {
            BusinessHourType = Domain.Core.BusinessHourType.Close;
            Day = dayOfWeek;
        }

        public void DayOpen(DayOfWeek dayOfWeek, TimeOnly openingHour, TimeOnly closingHour)
        {
            BusinessHourType = Domain.Core.BusinessHourType.Open;
            Day = dayOfWeek;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
        }

        public void DayOpenSchedule(TimeOnly openingHour, TimeOnly closingHour)
        {
            BusinessHourType = Domain.Core.BusinessHourType.Open;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
        }
    }
}