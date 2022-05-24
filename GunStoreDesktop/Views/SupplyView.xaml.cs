using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Views;

public partial class SupplyView : UserControl, INotifyPropertyChanged
{
    private ObservableCollection<Supplier> _suppliers;

    public ObservableCollection<Supplier> Suppliers
    {
        get => _suppliers;
        set
        {
            if (_suppliers != value)
            {
                _suppliers = value;
                OnPropertyChanged();
            }
        }
    }

    private Supplier _selectedSupplier;

    public Supplier SelectedSupplier
    {
        get => _selectedSupplier;
        set
        {
            _selectedSupplier = value;
            OnPropertyChanged();
        }
    }

    public SupplyView()
    {
        InitializeComponent();
        _suppliers = new ObservableCollection<Supplier>(DataFactory.GetMySqlDataFactory().Supplier.getSuppliers());
        Suppliers = _suppliers;
        DataContext = this;
        // updateView(ItemTypeCombobox.Text);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Supplier_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedSupplier = SupplierCombobox.SelectedItem as Supplier;
    }

    private void ItemTypeCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string? value = (ItemTypeCombobox.SelectedItem as ComboBoxItem).Content.ToString();
        Console.WriteLine(value);
        // updateView(value);
    }

    private void updateView(string value)
    {
        Console.WriteLine(value);
        switch (value)
        {
            case "Ammunition":
                TypeBox.IsEnabled = true;
                PackageSizeBox.IsEnabled = true;
                MagazineCapacityBox.IsEnabled = false;
                CaliberBox.IsEnabled = true;
                NameBox.IsEnabled = false;
                ColorBox.IsEnabled = false;
                FirearmCheckbox.IsEnabled = false;
                break;
            case "Equipment":
                TypeBox.IsEnabled = true;
                PackageSizeBox.IsEnabled = false;
                CaliberBox.IsEnabled = false;
                MagazineCapacityBox.IsEnabled = false;
                NameBox.IsEnabled = true;
                ColorBox.IsEnabled = true;
                FirearmCheckbox.IsEnabled = false;
                break;
            case "Weapon":
                TypeBox.IsEnabled = false;
                PackageSizeBox.IsEnabled = false;
                MagazineCapacityBox.IsEnabled = true;
                CaliberBox.IsEnabled = true;
                NameBox.IsEnabled = false;
                ColorBox.IsEnabled = false;
                FirearmCheckbox.IsEnabled = true;
                break;
        }
    }

    private void order()
    {
        
    }
}