using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;
using GunStoreDesktop.Windows;

namespace GunStoreDesktop.Views;

public partial class EmployeeView : UserControl, INotifyPropertyChanged
{
    private readonly ObservableCollection<Employee> _employees;
    private ObservableCollection<Employee> _filteredEmployees;

    public ObservableCollection<Employee> FilteredEmployees
    {
        get => _filteredEmployees;
        set
        {
            if (_filteredEmployees != value)
            {
                _filteredEmployees = value;
                OnPropertyChanged();
            }
        }
    }

    public EmployeeView()
    {   
        _employees = new ObservableCollection<Employee>(DataFactory.GetMySqlDataFactory().Employee.getEmployees());
        _filteredEmployees = _employees;
        FilteredEmployees = _employees;
        InitializeComponent();
        DataContext = this;
    }

    private void Row_OnClick(object sender, MouseButtonEventArgs e)
    {
        if ((sender as ListView)?.SelectedItem is Employee employeeToEdit)
        {
            EditEmployeeWindow editEmployeeWindow = new(employeeToEdit);
            editEmployeeWindow.ShowDialog();
            CollectionViewSource.GetDefaultView(_filteredEmployees).Refresh();
        }
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        Employee? employeeToDelete = ListView?.SelectedItem as Employee;
        if (employeeToDelete?.Equals(SettingsUtil.CurrentEmployee) ?? false)
        {
            return;
        }
        if (employeeToDelete != null)
        {
            DataFactory.GetMySqlDataFactory().Employee.deleteEmployeeById(employeeToDelete.Id);
            _employees.Remove(employeeToDelete);
            _filteredEmployees = _employees;
        }
    }

    private void Filter_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        FilteredEmployees = _employees;
        string filter = (sender as TextBox)?.Text ?? "";
        FilteredEmployees = new ObservableCollection<Employee>(FilteredEmployees.Where(employee => employee.Username.Contains(filter)).ToList());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        AddEmployeeWindow addEmployeeWindow = new(this);
        addEmployeeWindow.ShowDialog();
        CollectionViewSource.GetDefaultView(_filteredEmployees).Refresh();
    }

    public void addEmployeeToList(Employee addedEmployee)
    {
        _employees.Add(addedEmployee);
        FilteredEmployees = _employees;
    }
}