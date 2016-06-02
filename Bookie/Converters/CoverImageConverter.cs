using static System.String;

namespace Bookie.Converters
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    public class CoverImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string coverPath;

            var cover = value as string;
            if (IsNullOrEmpty(cover))
            {
                coverPath = @"C:\temp\NoCoverAvailable.png";
            }
            else
            {
                coverPath = File.Exists(cover) ? cover : @"C:\temp\NoCoverAvailable.png";
            }

            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(coverPath);
            image.EndInit();

            return image;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}