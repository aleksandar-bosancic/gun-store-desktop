using System;
using System.Windows;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;
using GunStoreDesktop.Views;

namespace GunStoreDesktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Employee CurrentEmployee { get; set; }
        public bool isAdmin { get; set; }
        
        public MainWindow(Employee employee)
        {
            CurrentEmployee = employee;
            isAdmin = CurrentEmployee.IsAdmin;
            SettingsUtil.CurrentEmployee = CurrentEmployee;
            SettingsUtil.SetStartupSettings(CurrentEmployee.Settings);
            InitializeComponent();
            DataContext = this;
        }

        private void LogoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
            Close();
        }
    }
}