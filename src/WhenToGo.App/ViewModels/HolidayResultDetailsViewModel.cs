using WhenToGo.App.Utils;

namespace WhenToGo.App.ViewModels
{
    public class HolidayResultDetailsViewModel : BaseViewModel
    {
        #region Member

        private IEnumerable<CountryHoliday> _retrievedHolidays;
        private Action _onRenderingDone;

        #endregion

        #region Constructor

        public HolidayResultDetailsViewModel(IEnumerable<CountryHoliday> holidays, DateTime dateFrom, DateTime dateTo, Action onRenderingDone)
        {
            _retrievedHolidays = holidays;
            SelectedDateFrom = dateFrom;
            SelectedDateTo = dateTo;
            FilterByPreferredCounties = true;
            _onRenderingDone = onRenderingDone;
        }

        #endregion


        #region Description

        public bool FilterByPreferredCounties
        {
            get => _filterByPreferredCounties;
            set
            {
                IsProcessingData = true;
                SetField(ref _filterByPreferredCounties, value);
                if (value)
                {
                    // filter CountryHolidays containing filtered counties
                    IEnumerable<CountryHoliday> filteredHolidays = _retrievedHolidays.Where(h =>h.NationWide || h.Subdivisions.Any(s => AppConstants.PreferredCounties.Any(c => c.Contains(s.Code))));
                    // apply counties filtering in holidays
                    Filter<List<Subdivision>> subdivisionFilter = new(subdivisions =>
                    {
                        return subdivisions.Where(sub => AppConstants.PreferredCounties.Any(c => c == sub.Code)).ToList();
                    });
                    filteredHolidays.ForEach(h => h.ApplySubdivisionFilter(subdivisionFilter));
                    Holidays = filteredHolidays;
                }
                else
                {
                    _retrievedHolidays.ForEach(h => h.ApplySubdivisionFilter(null));
                    Holidays = _retrievedHolidays;
                }
                IsProcessingData = false;
            }
        }
        private bool _filterByPreferredCounties;

        public bool IsProcessingData
        {
            get => _isProcessingData;
            private set => SetField(ref _isProcessingData, value);
        }
        private bool _isProcessingData;

        public DateTime SelectedDateFrom { get; }

        public DateTime SelectedDateTo { get; }

        public IEnumerable<CountryHoliday> Holidays
        {
            get => _holidays;
            private set => SetField(ref _holidays, value);
        }
        private IEnumerable<CountryHoliday> _holidays;

        public void DoRenderingDone()
        {
            _onRenderingDone?.Invoke();
        }

        #endregion
    }
}
