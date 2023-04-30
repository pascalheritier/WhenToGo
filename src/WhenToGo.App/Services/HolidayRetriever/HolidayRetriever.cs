using System.Text.Json;

namespace WhenToGo.App.Services
{

    internal class HolidayRetriever : IHolidayRetriever
    {
        #region Const

        private const string DisplayDateFormat = "dd.MM.yyyy";

        #endregion

        public HolidayRetriever()
        {
            _httpClient = new HttpClient();
        }

        #region Public & school holidays retrieval

        private HttpClient _httpClient;

        public async Task<IEnumerable<CountryHoliday>> GetPublicHolidays(
            string dateFrom,
            string dateTo,
            string countryIsoCode,
            string languageIsoCode)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://openholidaysapi.org/PublicHolidays?countryIsoCode={countryIsoCode}&languageIsoCode={languageIsoCode}&validFrom={dateFrom}&validTo={dateTo}"),
                Headers =
                {
                    { "accept", "text/json" }
                }
            };
            return await RequestHolidays(_httpClient, request);
        }

        public async Task<IEnumerable<CountryHoliday>> GetSchoolHolidays(
            string dateFrom,
            string dateTo,
            string countryIsoCode,
            string languageIsoCode)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://openholidaysapi.org/SchoolHolidays?countryIsoCode={countryIsoCode}&languageIsoCode={languageIsoCode}&validFrom={dateFrom}&validTo={dateTo}"),
                Headers =
                {
                    { "accept", "text/json" }
                }
            };
            return await RequestHolidays(_httpClient, request);
        }

        private async Task<IEnumerable<CountryHoliday>> RequestHolidays(HttpClient client, HttpRequestMessage request)
        {
            CountryHoliday[]? holidays = null;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                holidays = JsonSerializer.Deserialize<CountryHoliday[]>(body, options);
            }
            if (holidays != null)
                return holidays;
            return Enumerable.Empty<CountryHoliday>();
        }

        private void DisplayHolidays(IEnumerable<CountryHoliday> holidays)
        {
            foreach (var holiday in holidays)
            {
                Console.WriteLine($"{Environment.NewLine}------------------------------");
                Console.WriteLine($"Date from-to: {holiday.StartDate.ToString(DisplayDateFormat)} - {holiday.EndDate.ToString(DisplayDateFormat)}");
                Console.WriteLine($"Name: {holiday.Name.First().Text}");
                if (holiday.Subdivisions is not null && holiday.Subdivisions.Any())
                {
                    string subdivisionList = string.Join(", ", holiday.Subdivisions.Select(s => s.ShortName));
                    Console.WriteLine($"Counties: {subdivisionList}");
                }
                if (holiday.Comments is not null && holiday.Comments.Any())
                {
                    string comments = string.Join(", ", holiday.Comments.Select(s => s.Text));
                    Console.WriteLine($"Comments: {comments}");
                }
                Console.WriteLine($"------------------------------");
            }
        }

        #endregion
    }
}
