using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Bookie.Converters
{
    public class SingleLineTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            var s = (string)value;
            if (s.Contains("Jane Eyre"))
            {
                s = Regex.Replace(s, @"\r\n?|\n", " ");
            }
            //  s = s.Replace(Environment.NewLine, " ");
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}