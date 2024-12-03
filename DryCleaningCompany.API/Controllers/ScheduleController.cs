using DryCleaningCompany.API.Dtos;
using DryCleaningCompany.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaningCompany.API.Controllers
{
    [ApiController]
    [Route("schedule-calculator")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Calculate([FromQuery] ScheduleRequest request)
        {
            var result = await _scheduleService.CalculateNewSchedule(request.Date, request.Minutes);

            return Ok(new ScheduleResponse
            {
                InitialDate = result.InitialDate,
                MinutesToAdd = result.MinutesToAdd,
                FinalDate = result.FinalDate
            });
        }
    }
}