using System;
using System.Globalization;
using System.Windows.Data;

namespace Bookie.Converters
{
    public class StarRatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()) || value.ToString() == "0")
            {
                return "";
            }
            if (value.ToString() == "1")
            {
                return "\uE1CF";
            }
            if (value.ToString() == "2")
            {
                return "\uE1CF" + " " + "\uE1CF";
            }
            if (value.ToString() == "3")
            {
                return "\uE1CF" + " " + "\uE1CF" + " " + "\uE1CF";
            }
            if (value.ToString() == "4")
            {
                return "\uE1CF" + " " + "\uE1CF" + " " + "\uE1CF" + " " + "\uE1CF";
            }
            if (value.ToString() == "5")
            {
                return "\uE1CF" + " " + "\uE1CF" + " " + "\uE1CF" + " " + "\uE1CF" + " " + "\uE1CF";
            }

            return "\uE1CE" + "\uE1CE" + "\uE1CE" + "\uE1CE" + "\uE1CE";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "\uE1CE")
            {
                return 1;
            }
            if (value.ToString() == "\uE1CE" + "\uE1CE")
            {
                return 2;
            }
            if (value.ToString() == "\uE1CE" + "\uE1CE" + "\uE1CE")
            {
                return 3;
            }
            if (value.ToString() == "\uE1CE" + "\uE1CE" + "\uE1CE" + "\uE1CE")
            {
                return 4;
            }
            if (value.ToString() == "\uE1CE" + "\uE1CE" + "\uE1CE" + "\uE1CE" + "\uE1CE")
            {
                return 5;
            }
            return 0;
        }
    }
}