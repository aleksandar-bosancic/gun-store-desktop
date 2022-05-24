using System.Windows;
using System.Windows.Controls;
using GunStoreDesktop.Util;

namespace GunStoreDesktop.Controls;

public partial class LanguageMenuControl
{
    public LanguageMenuControl()
    {
        InitializeComponent();
    }
    
    private void LanguageSelection(object sender, RoutedEventArgs e)
    {
        string? code = (sender as MenuItem)?.Name;
        if (code != null)
        {
            TranslationSource.Instance.CultureInfo = new System.Globalization.CultureInfo(code);
        }
        LanguageMenu.IsOpen = false;
    }

    private void MenuOpen(object sender, RoutedEventArgs e)
    {
        LanguageMenu.IsOpen = true;
    }
}