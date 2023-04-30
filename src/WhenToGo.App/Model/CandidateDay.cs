namespace WhenToGo
{
    internal class CandidateDay
    {
        public DateTime Date { get; set; }

        public IEnumerable<CountryHoliday> OverlappingHolidays { get; set; }
    }
}
