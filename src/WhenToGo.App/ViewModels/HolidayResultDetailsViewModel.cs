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
                // first display the activity indicator while filtering is performed
                IsRenderingInProgress = true;

                SetField(ref _filterByPreferredCounties, value);
                Task.Run(() =>
                {
                    int? initialCount = Holidays?.Count();
                    if (value)
                    {
                        // filter CountryHolidays containing filtered counties
                        IEnumerable<CountryHoliday> filteredHolidays = _retrievedHolidays.Where(h => h.NationWide || h.Subdivisions.Any(s => AppConstants.PreferredCounties.Any(c => c.Contains(s.Code))));
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

                    // this is a workaround to hide the activity indicator when filtering is done and collection view has not changed in size
                    if (initialCount is not null && Holidays.Count() == initialCount)
                        IsRenderingInProgress = false;
            });
        }
        }
        private bool _filterByPreferredCounties;

        /// <summary>
        /// Due to MAUI slow rendering for nested collection views, we display an ActivityIndicator while rendering is in progress.
        /// </summary>
        public bool IsRenderingInProgress
        {
            get => _isRenderingInProgress;
            private set => SetField(ref _isRenderingInProgress, value);
        }
        private bool _isRenderingInProgress;

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
            this.IsRenderingInProgress = false;
        }

        #endregion
    }
}
