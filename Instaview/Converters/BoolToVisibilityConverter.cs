using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Instaview.Converters
{
    /// <summary>
    /// Converts a <see cref="bool"/> to a <see cref="Visibility"/> value and vice-versa.
    /// </summary>
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public static BoolToVisibilityConverter Instance { get; } = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visiblity = (Visibility)value;
            return visiblity == Visibility.Visible;
        }
    }
}
