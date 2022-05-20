using System;
using System.Globalization;
using System.Windows.Data;

namespace GunStoreDesktop.Util;

[ValueConversion(typeof(bool), typeof(string))]
public class BooleanToThemeConverter : IValueConverter
{
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return "Dark";
        }
        else
        {
            return "Light";
        }
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((string)value == "Dark")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}