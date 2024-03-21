using System.Globalization;


namespace MFBauphysikMobilMAUI.Converters
{

    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBeingDragged = (bool?)value;
            var result = (isBeingDragged ?? false) ? Color.FromArgb("#1e90ff") : Colors.Transparent;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
