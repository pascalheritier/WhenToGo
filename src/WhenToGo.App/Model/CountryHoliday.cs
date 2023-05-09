namespace WhenToGo
{
    public class CountryHoliday
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Type { get; set; }

        public List<HolidayName> Name { get; set; }

        public bool NationWide { get; set; }

        public List<Subdivision> Subdivisions { get; set; }

        public List<HolidayComment> Comments { get; set; }

        public string CurrentName => Name.FirstOrDefault()?.Text;
    }

    public class HolidayName
    {
        public string Text { get; set; }
        public string Language { get; set; }
    }

    public class HolidayComment
    {
        public string Text { get; set; }
        public string Language { get; set; }
    }

    public class Subdivision
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
    }
}
