using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Input;
using WhenToGo.App.Services;

namespace WhenToGo.App.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        #region Members

        private IHolidayRetriever _holidayRetriever; 

        #endregion

        #region Constructor

        public MainViewModel()
        {
            _holidayRetriever = new HolidayRetriever();
        }

        #endregion

        #region Holidays

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
                    string dateFrom = "2023-04-13";
                    string dateTo = "2023-04-17";
                    string countryIsoCode = "CH";
                    string languageIsoCode = "FR";
                    Holidays = await _holidayRetriever.GetPublicHolidays(dateFrom, dateTo, countryIsoCode, languageIsoCode);
                });
                return _commandGetHolidays;
            }
        }
        private ICommand _commandGetHolidays; 

        #endregion
    }
}
