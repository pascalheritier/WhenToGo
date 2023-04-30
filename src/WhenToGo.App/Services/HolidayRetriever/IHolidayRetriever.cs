using System.Text.Json;

namespace WhenToGo.App.Services
{
    internal interface IHolidayRetriever
    {
        Task<IEnumerable<CountryHoliday>> GetPublicHolidays(
            string dateFrom,
            string dateTo,
            string countryIsoCode,
            string languageIsoCode);

        Task<IEnumerable<CountryHoliday>> GetSchoolHolidays(
            string dateFrom,
            string dateTo,
            string countryIsoCode,
            string languageIsoCode);
    }
}
