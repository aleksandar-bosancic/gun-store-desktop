using System;
using System.Globalization;
using System.Resources;
using System.Windows.Data;

namespace GunStoreDesktop.Util;

[ValueConversion(typeof(bool), typeof(string))]
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(string))
        {
            throw new InvalidOperationException("the target must be string");
        }

        if ((bool)value)
        {
            return "Visible";
        }
        else
        {
            return "Hidden";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(bool))
        {
            throw new InvalidOperationException("the target must be boolean");
        }

        if ((string)value == "Visible")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}