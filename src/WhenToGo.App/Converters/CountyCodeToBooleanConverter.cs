using System.Globalization;
using WhenToGo.App.Utils;

namespace WhenToGo.App.Converters
{
    public class CountyCodeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is Binding binding)
            {
                // get binding value programatically
                parameter = Utilities.GetPropertyValue(binding.Source, binding.Path);
                // check binding value
                if (parameter is bool isFilterActive)
                    if (!isFilterActive)
                        return true;
            }

            if (value is string countyCode)
            {
                return AppConstants.PreferredCounties.Any(code => countyCode == code);
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
