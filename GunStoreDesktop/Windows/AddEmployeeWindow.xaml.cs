using System.Windows;
using System.Windows.Controls;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;
using GunStoreDesktop.Views;

namespace GunStoreDesktop.Windows;

public partial class AddEmployeeWindow : Window
{
    public Employee Employee { get; set; }
    public EmployeeView owner;
    public AddEmployeeWindow(DependencyObject sender)
    {
        InitializeComponent();
        owner = (EmployeeView)sender;
        Employee = new Employee();
        DataContext = this;
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!StringsUtil.isAllowed(Employee.Username) && !StringsUtil.isAllowed(Employee.Password))
        {
            MessageBox.Show(this, $"{TranslationSource.Instance["InvalidCharactersText"]}", 
                $"{TranslationSource.Instance["InvalidCharactersCaption"]}" , MessageBoxButton.OK, MessageBoxImage.Warning);
            UsernameTextBox.Clear();
            PasswordTextBox.Clear();
            return;
        }
        if (Employee.Username != string.Empty && Employee.Password != string.Empty)
        {
            Employee addedEmployee = DataFactory.GetMySqlDataFactory().Employee.saveEmployee(Employee);
            owner.addEmployeeToList(addedEmployee);
        }
        Close();
    }
}