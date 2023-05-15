namespace WhenToGo.App.ViewModels
{
    public class HolidayResultDetailsViewModel : BaseViewModel
    {
        #region Member

        private IEnumerable<CountryHoliday> _retrievedHolidays;
        private Action _onRenderingDone;

        #endregion

        #region Consts

        IEnumerable<string> PreferredCounties
        {
            get
            {
                yield return "CH-BE";
                yield return "CH-GE";
                yield return "CH-FR";
                yield return "CH-JU";
                yield return "CH-NE";
                yield return "CH-VD";
                yield return "CH-VS";
            }
        }

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
                SetField(ref _filterByPreferredCounties, value);
                if (value)
                    Holidays = _retrievedHolidays.Where(h =>h.NationWide || h.Subdivisions.Any(s => PreferredCounties.Any(c => c.Contains(s.Code))));
                else
                    Holidays = _retrievedHolidays;
            }
        }
        private bool _filterByPreferredCounties;

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
