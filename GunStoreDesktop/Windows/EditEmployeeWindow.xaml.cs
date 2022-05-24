using System;
using System.Windows;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;

namespace GunStoreDesktop.Windows;

public partial class EditEmployeeWindow : Window
{
    private Employee _employee;
    public Employee Employee
    {
        get => _employee;
        set => _employee = value;
    }
    
    public EditEmployeeWindow(Employee employee)
    {
        InitializeComponent();
        _employee = employee;
        DataContext = this;
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {

        if (!StringsUtil.isAllowed(Employee.Username) && !StringsUtil.isAllowed(Employee.Password))
        {
            MessageBox.Show(this, $"{TranslationSource.Instance["InvalidCharactersText"]}",
                $"{TranslationSource.Instance["InvalidCharactersCaption"]}", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            UsernameTextBox.Clear();
            PasswordTextBox.Clear();
            return;
        }

        if (Employee.Username != string.Empty && Employee.Password != string.Empty)
        {
            DataFactory.GetMySqlDataFactory().Employee.updateEmployeeById(Employee);
        }
        Close();
    }
}