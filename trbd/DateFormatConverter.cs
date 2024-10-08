using System;
using System.Globalization;
using System.Windows.Data;

namespace trbd
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // Change the format as needed, e.g., "dd/MM/yyyy"
                return dateTime.ToString("dd/MM/yyyy");
            }
            return value; // Return the original value if it's not a DateTime
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Implement if you need to convert back to DateTime
            throw new NotImplementedException();
        }
    }

}
