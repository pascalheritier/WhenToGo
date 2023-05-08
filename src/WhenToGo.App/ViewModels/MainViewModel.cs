using System.Windows.Input;
using WhenToGo.App.Services;

namespace WhenToGo.App.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        #region Consts
        
        private const string ApiDateTimeFormat = "yyyy-MM-dd";

        #endregion

        #region Members

        private IHolidayRetriever _holidayRetriever;

        public bool IsProcessingData { get; set; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            _holidayRetriever = new HolidayRetriever();
            SelectedDateFrom = DateTime.Today;
            SelectedDateTo = DateTime.Today;
        }

        #endregion

        #region Holidays

        public DateTime SelectedDateFrom { get; set; }
        public DateTime SelectedDateTo { get; set; }

        public IEnumerable<CountryHoliday> Holidays
        {
            get => _holidays;
            private set => SetField(ref _holidays, value);
        }
        private IEnumerable<CountryHoliday> _holidays;

        public ICommand CommandGetHolidays
        {
            get
            {
                _commandGetHolidays ??= new Command(async () =>
                {
                    IsProcessingData = true;
                    string dateFrom = SelectedDateFrom.ToString(ApiDateTimeFormat);
                    string dateTo = SelectedDateTo.ToString(ApiDateTimeFormat);
                    string countryIsoCode = "CH";
                    string languageIsoCode = "FR";
                    List<CountryHoliday> countryHolidays = new List<CountryHoliday>();
                    countryHolidays.AddRange(await _holidayRetriever.GetPublicHolidays(dateFrom, dateTo, countryIsoCode, languageIsoCode));
                    countryHolidays.AddRange(await _holidayRetriever.GetSchoolHolidays(dateFrom, dateTo, countryIsoCode, languageIsoCode));
                    Holidays = countryHolidays;
                    IsProcessingData = false;
                });
                return _commandGetHolidays;
            }
        }
        private ICommand _commandGetHolidays; 

        #endregion
    }
}
