namespace WhenToGo
{
    internal class CountryHoliday
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

    internal class HolidayName
    {
        public string Text { get; set; }
        public string Language { get; set; }
    }

    internal class HolidayComment
    {
        public string Text { get; set; }
        public string Language { get; set; }
    }

    internal class Subdivision
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
    }
}
