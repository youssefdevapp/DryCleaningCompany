namespace DryCleaningCompany.API.Dtos
{
    public class ScheduleResponse
    {
        public DateTime InitialDate { get; set; }
        public int MinutesToAdd { get; set; }
        public DateTime FinalDate { get; set; }
    }
}