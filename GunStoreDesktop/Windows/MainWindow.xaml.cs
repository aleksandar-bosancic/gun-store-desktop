﻿using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace GunStoreDesktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDarkTheme;
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
    }
}