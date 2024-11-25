namespace DryCleaningCompany.Domain.Entities
{
    public class Schedule : EntityBase
    {
        public DateTime InitialDate { get; private set; }
        public int MinutesToAdd { get; private set; }
        public DateTime FinalDate { get; set; }

        public Schedule(DateTime initialDate, int minutesToAdd)
        {
            InitialDate = initialDate;
            MinutesToAdd = minutesToAdd;
            FinalDate = InitialDate.AddMinutes(minutesToAdd);
        }
    }
}