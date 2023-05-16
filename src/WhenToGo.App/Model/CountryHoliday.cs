namespace WhenToGo
{
    public class CountryHoliday
    {
        #region API Model

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Type { get; set; }

        public List<HolidayName> Name { get; set; }

        public bool NationWide { get; set; }

        public List<Subdivision> Subdivisions 
        { 
            get
            {
                if (IsSubdivisionFilterActive)
                    return _subdivisionFilter.FilterValues(_subdivisions);
                return _subdivisions;
            }
            set => _subdivisions = value; 
        }
        private List<Subdivision> _subdivisions;


        public List<HolidayComment> Comments { get; set; }

        public string CurrentName => Name.FirstOrDefault()?.Text;

        #endregion

        #region Filtering

        public bool IsSubdivisionFilterActive => _subdivisionFilter != null;

        private Filter<List<Subdivision>> _subdivisionFilter;

        public void ApplySubdivisionFilter(Filter<List<Subdivision>> subdivisionFilter)
        {
            _subdivisionFilter = subdivisionFilter;
        }

        #endregion
    }
}
