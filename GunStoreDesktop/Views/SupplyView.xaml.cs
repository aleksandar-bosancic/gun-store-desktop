using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;

namespace GunStoreDesktop.Views;

public partial class SupplyView : UserControl, INotifyPropertyChanged
{
    private string? selectedItemType = "";
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public string Type { get; set; }
    public string ItemColor { get; set; }
    public string ItemName { get; set; }
    public string Caliber { get; set; }

    public int PackageSize { get; set; }
    public int MagazineSize { get; set; }
    public bool IsFirearm { get; set; }
    public int ExistingItemId { get; set; }
    public int ExistingItemAmount { get; set; }

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

    private Supplier? _selectedSupplier;

    public Supplier? SelectedSupplier
    {
        get => _selectedSupplier;
        set
        {
            _selectedSupplier = value;
            OnPropertyChanged();
        }
    }

    private List<Item> _items;

    public SupplyView()
    {
        InitializeComponent();
        _suppliers = new ObservableCollection<Supplier>(DataFactory.GetMySqlDataFactory().Supplier.getSuppliers());
        Suppliers = _suppliers;
        _items = DataFactory.GetMySqlDataFactory().Item.getItems();
        DataContext = this;
        ExistingItemGrid.Visibility = Visibility.Hidden;
        updateView();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Supplier_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedSupplier = SupplierCombobox.SelectedItem as Supplier ?? (Supplier)SupplierCombobox.Items[0];
    }

    private void ItemTypeCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedItemType = (ItemTypeCombobox.SelectedItem as ComboBoxItem)?.Content.ToString();
        clearValues();
        updateView();
    }

    private void clearValues()
    {
        ManufacturerBox.Clear();
        ModelBox.Clear();
        PriceBox.Clear();
        AmountBox.Clear();
        ExistingAmountBox.Clear();
        IdBox.Clear();
        TypeBox.Clear();
        NameBox.Clear();
        ColorBox.Clear();
        PackageSizeBox.Clear();
        MagazineCapacityBox.Clear();
        CaliberBox.Clear();
        FirearmCheckbox.IsChecked = false;
        Manufacturer = "";
        Caliber = "";
        Model = "";
        ItemColor = "";
        Type = "";
        IsFirearm = false;
        ItemName = "";
        Price = 0;
        Amount = 0;
        MagazineSize = 0;
        PackageSize = 0;
    }

    private void updateView()
    {
        switch (selectedItemType)
        {
            case "Ammunition":
                TypeBox.IsEnabled = true;
                PackageSizeBox.Visibility = Visibility.Visible;
                PackageSizeBox.IsEnabled = true;
                MagazineCapacityBox.Visibility = Visibility.Hidden;
                MagazineCapacityBox.IsEnabled = false;
                CaliberBox.IsEnabled = true;
                NameBox.IsEnabled = false;
                ColorBox.IsEnabled = false;
                FirearmCheckbox.IsEnabled = false;
                NewOrderButton.IsEnabled = true;
                ExistingOrderButton.IsEnabled = true;
                break;
            case "Equipment":
                TypeBox.IsEnabled = true;
                PackageSizeBox.IsEnabled = false;
                CaliberBox.IsEnabled = false;
                MagazineCapacityBox.IsEnabled = false;
                PackageSizeBox.IsEnabled = false;
                NameBox.IsEnabled = true;
                ColorBox.IsEnabled = true;
                FirearmCheckbox.IsEnabled = false;
                NewOrderButton.IsEnabled = true;
                ExistingOrderButton.IsEnabled = true;
                break;
            case "Weapon":
                TypeBox.IsEnabled = false;
                PackageSizeBox.Visibility = Visibility.Hidden;
                PackageSizeBox.IsEnabled = false;
                MagazineCapacityBox.Visibility = Visibility.Visible;
                MagazineCapacityBox.IsEnabled = true;
                CaliberBox.IsEnabled = true;
                NameBox.IsEnabled = false;
                ColorBox.IsEnabled = false;
                FirearmCheckbox.IsEnabled = true;
                NewOrderButton.IsEnabled = true;
                ExistingOrderButton.IsEnabled = true;
                break;
            default:
                NewOrderButton.IsEnabled = false;
                ExistingOrderButton.IsEnabled = false;
                break;
        }
    }

    private void order(bool isNewOrder)
    {
        if (!isNewOrder)
        {
            existingOrder();
            return;
        }

        if (!checkBasicFields())
        {
            return;
        }

        if (SelectedSupplier is null)
        {
            MessageBox.Show($"{TranslationSource.Instance["SupplierNotSelectedText"]}",
                $"{TranslationSource.Instance["SupplierNotSelectedCaption"]}", MessageBoxButton.OK,
                MessageBoxImage.Exclamation);
            return;
        }
        
        switch (selectedItemType)
        {
            case "Ammunition":
                orderAmmo();
                break;
            case "Equipment":
                orderEquipment();
                break;
            case "Weapon":
                orderWeapon();
                break;
        }
    }

    private void existingOrder()
    {
        Item? itemToOrder = _items.Find(item => item.Id == ExistingItemId);
        if (ExistingItemId == 0 || ExistingItemAmount == 0)
        {
            showEmptyFieldsMessage();
            return;
        }

        if (itemToOrder is null)
        {
            MessageBox.Show($"{TranslationSource.Instance["NoItemText"]}",
                $"{TranslationSource.Instance["NoItemCaption"]}", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        DataFactory.GetMySqlDataFactory().Item.updateInStockById(ExistingItemId, itemToOrder.InStock + ExistingItemAmount);
        createOrder(ExistingItemId);
        clearValues();
    }

    private void orderWeapon()
    {
        if (Caliber.Equals(string.Empty) || MagazineSize == 0)
        {
            showEmptyFieldsMessage();
            return;
        }

        Item addedItem = addItem();
        DataFactory.GetMySqlDataFactory().Weapon.saveWeapon(new Weapon()
        {
            Caliber = Caliber,
            IsFirearm = IsFirearm,
            ItemId = addedItem.Id,
            MagazineCapacity = MagazineSize
        });
        clearValues();
    }

    private void orderEquipment()
    {
        if (Type.Equals(string.Empty) || ItemColor.Equals(string.Empty) || ItemName.Equals(string.Empty))
        {
            showEmptyFieldsMessage();
            return;
        }

        Item addedItem = addItem();
        DataFactory.GetMySqlDataFactory().Equipment.saveEquipment(new Equipment()
        {
            Colour = ItemColor,
            Name = ItemName,
            Type = Type,
            Id = addedItem.Id
        });
        clearValues();
    }

    private void orderAmmo()
    {
        if (Type.Equals(string.Empty) || Caliber.Equals(string.Empty) || PackageSize == 0)
        {
            showEmptyFieldsMessage();
            return;
        }

        Item addedItem = addItem();
        DataFactory.GetMySqlDataFactory().Ammunition.saveAmmunition(new Ammunition()
        {
            Caliber = Caliber,
            MunitionType = Type,
            PackageSize = PackageSize,
            Id = addedItem.Id
        });
    }

    private Item addItem()
    {
        Item addedItem = DataFactory.GetMySqlDataFactory().Item.saveItem(new Item()
        {
            Manufacturer = Manufacturer,
            Model = Model,
            Price = Price,
            InStock = Amount
        });
        createOrder(addedItem.Id);
        return addedItem;
    }

    private void createOrder(int itemId)
    {
        PurchaseOrder purchaseOrder = DataFactory.GetMySqlDataFactory().PurchaseOrder.save(new PurchaseOrder()
        {
            SupplierId = SelectedSupplier!.Id,
            DateTime = DateTime.Now
        });
        DataFactory.GetMySqlDataFactory().ItemPurchaseOrder.save(new ItemPurchaseOrder()
        {
            ItemId = itemId,
            PurchaseOrderId = purchaseOrder.Id,
            Amount = Amount,
            SupplierPrice = (Price - Price * 0.1) * Amount
        });
    }

    private bool checkBasicFields()
    {
        if (Manufacturer.Equals(string.Empty) || Model.Equals(string.Empty) || Price == 0 || Amount == 0)
        {
            showEmptyFieldsMessage();
            return false;
        }

        return true;
    }

    private void showEmptyFieldsMessage()
    {
        MessageBox.Show($"{TranslationSource.Instance["FieldsEmptyText"]}",
            $"{TranslationSource.Instance["FieldsEmptyText"]}", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private void ExistingItemCheckBox_OnClick(object sender, RoutedEventArgs e)
    {
        clearValues();
        if (ExistingItemCheckBox.IsChecked ?? false)
        {
            ExistingItemGrid.Visibility = Visibility.Visible;
            NewItemGrid.Visibility = Visibility.Hidden;
        }

        else
        {
            ExistingItemGrid.Visibility = Visibility.Hidden;
            NewItemGrid.Visibility = Visibility.Visible;
        }
    }

    private void PriceBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void NewOrderButton_OnClick(object sender, RoutedEventArgs e)
    {
        order(true);
    }

    private void ExistingOrderButton_OnClick(object sender, RoutedEventArgs e)
    {
        order(false);
    }
}