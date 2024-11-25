using DryCleaningCompany.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaningCompany.API.Controllers
{
    [ApiController]
    [Route("setup-calculator")]
    public class SetupController : Controller
    {
        private readonly ISetupService _setupService;

        public SetupController(ISetupService setupService)
        {
            _setupService = setupService;
        }

        [HttpPost]
        [Route("Days-Schedule")]
        public async Task<IActionResult> DaysSchedule(TimeOnly openingHour, TimeOnly closingHour)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Days")]
        public async Task<IActionResult> Days(DayOfWeek dayOfWeek, TimeOnly openingHour, TimeOnly closingHour)
        {
            await _setupService.DaysOpen(dayOfWeek, openingHour, closingHour);
            return Ok();
        }

        [HttpPut]
        [Route("Dates")]
        public async Task<IActionResult> Dates(DateOnly date, TimeOnly openingHour, TimeOnly closingHour)
        {
            await _setupService.DatesOpen(date, openingHour, closingHour);
            return Ok();
        }

        [HttpPut]
        [Route("Days-Close")]
        public async Task<IActionResult> DaysClose(params DayOfWeek[] daysOfWeek)
        {
            await _setupService.DaysClose(daysOfWeek);
            return Ok();
        }

        [HttpPut]
        [Route("Dates-Close")]
        public async Task<IActionResult> DatesClose(params DateOnly[] dates)
        {
            await _setupService.DatesClose(dates);
            return Ok();
        }
    }
}