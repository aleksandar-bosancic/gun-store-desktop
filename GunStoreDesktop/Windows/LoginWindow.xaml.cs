using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;
// ReSharper disable MemberCanBePrivate.Global

namespace GunStoreDesktop.Windows;

public partial class LoginWindow
{
    // DataBinding to UsernameBox
    public string? Username { get; set; }
    public string? Password { get; set; }
    
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void LoginButtonBaseOnClick(object sender, RoutedEventArgs e)
    {
        List<Employee> employees = DataFactory.GetMySqlDataFactory().Employee.getEmployees();
        Employee? employee = Array.Find(employees.ToArray(), 
            employee => employee.Username == Username && employee.Password == Password);
        if (employee != null)
        {
            MainWindow mainWindow = new()
            {
                CurrentEmployee = employee
            };
            mainWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show(this, $"{TranslationSource.Instance["FailedLoginText"]}", 
                $"{TranslationSource.Instance["FailedLoginCaption"]}", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null)
        {
            Password = (sender as PasswordBox)?.Password;
        }
    }
}