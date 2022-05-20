using System;
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

namespace GunStoreDesktop.Views;

public partial class EmployeeView : UserControl, INotifyPropertyChanged
{
    private ObservableCollection<Employee> _employees;
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
        Console.WriteLine(((sender as ListView).SelectedItem as Employee).Username);

    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine((ListView.SelectedItem as Employee).Username);
    }

    private void Filter_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        FilteredEmployees = _employees;
        string filter = (sender as TextBox).Text;
        FilteredEmployees = new ObservableCollection<Employee>(FilteredEmployees.Where(employee => employee.Username.Contains(filter)).ToList());
        Console.WriteLine(FilteredEmployees.Count);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}