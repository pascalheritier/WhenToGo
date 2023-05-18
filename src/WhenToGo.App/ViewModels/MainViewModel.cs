using System.Windows.Input;
using WhenToGo.App.Services;
using WhenToGo.App.Utils;
using WhenToGo.App.Views;

namespace WhenToGo.App.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        #region Consts

        private const string ApiDateTimeFormat = "yyyy-MM-dd";

        #endregion

        #region Members

        private IHolidayRetriever _holidayRetriever;

        #endregion

        #region Constructor

        public MainViewModel()
        {
            _holidayRetriever = new HolidayRetriever();
            SelectedDateFrom = DateTime.Today;
            SelectedDateTo = DateTime.Today;
        }

        #endregion

        #region Description

        public HolidayResultDetailsViewModel HolidayResultDetailsViewModel { get; set; }


        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetField(ref _errorMessage, value);
        }
        private string _errorMessage;

        /// <summary>
        /// Indicates if application is retrieving and processing data from API.
        /// </summary>
        public bool IsProcessingData
        {
            get => _isProcessingData;
            private set => SetField(ref _isProcessingData, value);
        }
        private bool _isProcessingData;

        public DateTime SelectedDateFrom { get; set; }

        public DateTime SelectedDateTo { get; set; }

        public ICommand CommandGetHolidays
        {
            get
            {
                _commandGetHolidays ??= new Command(async () =>
                {
                    try
                    {
                        ErrorMessage = null;
                        IsProcessingData = true;
                        string dateFrom = SelectedDateFrom.ToString(ApiDateTimeFormat);
                        string dateTo = SelectedDateTo.ToString(ApiDateTimeFormat);
                        string countryIsoCode = "CH";
                        string languageIsoCode = "FR";
                        List<CountryHoliday> countryHolidays = new List<CountryHoliday>();
                        countryHolidays.AddRange(await _holidayRetriever.GetPublicHolidays(dateFrom, dateTo, countryIsoCode, languageIsoCode));
                        countryHolidays.AddRange(await _holidayRetriever.GetSchoolHolidays(dateFrom, dateTo, countryIsoCode, languageIsoCode));
                        HolidayResultDetailsViewModel = new(countryHolidays, SelectedDateFrom, SelectedDateTo, () => this.IsProcessingData = false);
                        var navigationParameter = new Dictionary<string, object>
                        {
                            {
                                AppConstants.HolidayDetailParameter,
                                HolidayResultDetailsViewModel
                            }
                        };
                        await Shell.Current.GoToAsync($"{nameof(HolidayResultDetailsView).ToLower()}", navigationParameter);
                    }
                    catch (Exception ex)
                    {
                        IsProcessingData = false;
                        ErrorMessage = ex.Message;
                    }
                });
                return _commandGetHolidays;
            }
        }
        private ICommand _commandGetHolidays;

        #endregion
    }
}
