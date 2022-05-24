using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Media;
using GunStoreDesktop.Data;
using GunStoreDesktop.Data.Model;
using MaterialDesignThemes.Wpf;

namespace GunStoreDesktop.Util;

public static class SettingsUtil
{
    public static Employee CurrentEmployee = new ();
    public static EmployeeSettings StringToEmployeeSettings(string settings)
    {
        if (settings != string.Empty)
        {
            return JsonSerializer.Deserialize<EmployeeSettings>(settings) ?? new EmployeeSettings();
        }
        return new EmployeeSettings();
    }

    public static string EmployeeSettingsToString(EmployeeSettings settings)
    {
        return JsonSerializer.Serialize(settings);
    }

    public static void SetStartupSettings(EmployeeSettings settings)
    {
        TranslationSource.Instance.CultureInfo = new System.Globalization.CultureInfo(settings.Language);
        PaletteHelper paletteHelper = new();
        ITheme theme = paletteHelper.GetTheme();
        Color primary = (Color)ColorConverter.ConvertFromString(settings.PrimaryColor);
        Color accent = (Color)ColorConverter.ConvertFromString(settings.AccentColor);
        theme.SetBaseTheme(settings.Theme == "Dark" ? Theme.Dark : Theme.Light);
        theme.SetPrimaryColor(primary);
        theme.SetSecondaryColor(accent);
        paletteHelper.SetTheme(theme);
    }
}