using WhenToGo.App.Services;
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
            _onRenderingDone = onRenderingDone;

            // filters
            _subdivisionFilter = new(subdivisions =>
            {
                return subdivisions.Where(sub => AppConstants.PreferredCounties.Any(c => c == sub.Code)).ToList();
            });
            _filterShowSchoolHolidays = true;
            _filterShowPublicHolidays = true;
            FilterByPreferredCounties = true;
        }

        #endregion

        #region Filters

        private Filter<List<Subdivision>> _subdivisionFilter;

        public bool FilterByPreferredCounties
        {
            get => _filterByPreferredCounties;
            set
            {
                SetField(ref _filterByPreferredCounties, value);
                this.ApplyFilters();
            }
        }
        private bool _filterByPreferredCounties;

        public bool FilterShowSchoolHolidays
        {
            get => _filterShowSchoolHolidays;
            set
            {
                SetField(ref _filterShowSchoolHolidays, value);
                this.ApplyFilters();
            }
        }
        private bool _filterShowSchoolHolidays;

        public bool FilterShowPublicHolidays
        {
            get => _filterShowPublicHolidays;
            set
            {
                SetField(ref _filterShowPublicHolidays, value);
                this.ApplyFilters();
            }
        }
        private bool _filterShowPublicHolidays;

        private void ApplyFilters()
        {
            // first display the activity indicator while filtering is performed
            IsRenderingInProgress = true;

            Task.Run(() =>
            {
                // for rendering only
                int? initialCount = Holidays?.Count();

                // filter by type
                IEnumerable<CountryHoliday> filteredHolidays = this.FilterHolidayByType(_retrievedHolidays, _filterShowSchoolHolidays, _filterShowPublicHolidays);

                // filter by counties
                if (_filterByPreferredCounties)
                {
                    // filter by CountryHolidays containing filtered counties only
                    filteredHolidays = filteredHolidays.Where(h => h.NationWide || h.Subdivisions.Any(s => AppConstants.PreferredCounties.Any(c => c.Contains(s.Code))));
                    // apply counties filtering in holidays
                    filteredHolidays.ForEach(h => h.ApplySubdivisionFilter(_subdivisionFilter));
                }
                else
                {
                    // reset the subdivision filter if any was applied previously
                    filteredHolidays.ForEach(h => h.ApplySubdivisionFilter(null));
                }
                Holidays = filteredHolidays;

                // this is a workaround to hide the activity indicator when filtering is done and collection view has not changed in size
                if (initialCount is not null && Holidays.Count() == initialCount || initialCount == 0 || Holidays.Count() == 0)
                    IsRenderingInProgress = false;
            });
        }

        private IEnumerable<CountryHoliday> FilterHolidayByType(IEnumerable<CountryHoliday> holidays, bool showSchoolHoliday, bool showPublicHoliday)
        {
            if (showSchoolHoliday && showPublicHoliday)
                return holidays;
            if (!showSchoolHoliday && showPublicHoliday)
                return holidays.Where(h => h.Type == HolidayRetriever.HolidayTypePublicName);
            if (showSchoolHoliday && !showPublicHoliday)
                return holidays.Where(h => h.Type == HolidayRetriever.HolidayTypeSchoolName);
            return Enumerable.Empty<CountryHoliday>();
        }

        #endregion

        #region Description

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