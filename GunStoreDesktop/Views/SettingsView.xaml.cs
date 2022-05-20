using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GunStoreDesktop.Data;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Util;
using GunStoreDesktop.Windows;
using MaterialDesignThemes.Wpf;

namespace GunStoreDesktop.Views;

public partial class SettingsView : UserControl, INotifyPropertyChanged
{
    private MainWindow parent;
    public event PropertyChangedEventHandler? PropertyChanged;
    private Color _primaryColor;
    private Color _accentColor;
    
    private string _currentTheme;

    public string CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme != value)
            {
                _currentTheme = value;
                OnPropertyChanged();
            }
        }
    }
    
    private EmployeeSettings _settings;
    public EmployeeSettings Settings
    {
        get => _settings;
        set
        {
            if (_settings != value)
            {
                _settings = value;
                OnPropertyChanged();
            }
        }
    }
    
    private string _currentLanguage;
    public string CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            if (_currentLanguage != value)
            {
                _currentLanguage = value;
                OnPropertyChanged();
            }
        }
    }

    private Color _selectedColor;

    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (_selectedColor != value)
            {
                _selectedColor = value;
                if (ColorSwitchToggle.IsChecked ?? false)
                {
                    _accentColor = _selectedColor;
                }
                else
                {
                    _primaryColor = _selectedColor;
                }

                OnPropertyChanged();
            }
        }
    }

    public SettingsView()
    {
        // ReSharper disable once AssignNullToNotNullAttribute
        parent = (MainWindow)Application.Current.MainWindow;
        Settings = parent.CurrentEmployee.Settings;
        _primaryColor = (Color)ColorConverter.ConvertFromString(Settings.PrimaryColor);
        _accentColor = (Color)ColorConverter.ConvertFromString(Settings.AccentColor);
        _currentLanguage = Settings.Language;
        CurrentTheme = Settings.Theme;
        InitializeComponent();
        SelectedColor = (Color)ColorConverter.ConvertFromString(Settings.PrimaryColor);
        DataContext = this;
    }

    private void LanguageListClick(object sender, MouseButtonEventArgs e)
    {
        string code = ((sender as ListView)?.SelectedItem as ListViewItem)?.Name ?? "en";
        CurrentLanguage = code;
        TranslationSource.Instance.CultureInfo = new System.Globalization.CultureInfo(code);
    }


    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName == "SelectedColor")
        {
            PaletteHelper paletteHelper = new();
            ITheme theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(_primaryColor);
            theme.SetSecondaryColor(_accentColor);
            paletteHelper.SetTheme(theme);
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ColorSwitchToggle_OnClick(object sender, RoutedEventArgs e)
    {
        if ((sender as ToggleButton)?.IsChecked ?? false)
        {
            SelectedColor = _accentColor;
        }
        else
        {
            SelectedColor = _primaryColor;
        }
    }

    private void ThemeSwitchToggle_OnClick(object sender, RoutedEventArgs e)
    {
        PaletteHelper paletteHelper = new();
        ITheme theme = paletteHelper.GetTheme();
        if ((sender as ToggleButton)?.IsChecked ?? false)
        {
            CurrentTheme = "Dark";
            theme.SetBaseTheme(Theme.Dark);
            paletteHelper.SetTheme(theme);
        }
        else
        {
            CurrentTheme = "Light";
            theme.SetBaseTheme(Theme.Light);
            paletteHelper.SetTheme(theme);
        }
    }

    private void SaveSettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        Settings.Language = _currentLanguage;
        Settings.PrimaryColor = _primaryColor.ToString();
        Settings.AccentColor = _accentColor.ToString();
        parent.CurrentEmployee.Settings = Settings;
        DataFactory.GetMySqlDataFactory().Employee.updateEmployeeSettingsById(parent.CurrentEmployee);
    }
}