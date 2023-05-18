using System.Globalization;
using WhenToGo.App.Utils;

namespace WhenToGo.App.Converters
{
    internal class IsoCodeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string stringValue)
            {
                return stringValue.ToLower().Replace("-", "_") + Utilities.PngFileExtension; // As per doc, use png extension even for svg files: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/images/images
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
