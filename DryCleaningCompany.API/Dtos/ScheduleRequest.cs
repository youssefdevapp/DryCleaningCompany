using Microsoft.AspNetCore.Mvc;

namespace DryCleaningCompany.API.Dtos
{
    public class ScheduleRequest
    {
        [FromQuery]
        public int Minutes { get; set; }

        [FromQuery]
        public DateTime Date { get; set; }
    }
}