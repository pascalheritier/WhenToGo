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
                return stringValue.ToLower().Replace("-", "_") + Utilities.SvgFileExtension;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
