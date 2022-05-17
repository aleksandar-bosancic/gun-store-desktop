using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;
using MaterialDesignThemes.Wpf;

namespace GunStoreDesktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Employee? CurrentEmployee { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DarkButtonOnClick(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Dark);
            paletteHelper.SetTheme(theme);
        }

        private void LightButtonOnClick(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void GreenButtonOnClick(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(Colors.Lime);
            theme.SetSecondaryColor(Colors.MediumPurple);
            paletteHelper.SetTheme(theme);
        }

        private void YellowButtonOnClick(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(Colors.Chartreuse);
            theme.SetSecondaryColor(Colors.Blue);
            paletteHelper.SetTheme(theme);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AAAAAAAAAAAAA");
            (sender as ToggleButton).IsChecked = true;
        }
    }
}