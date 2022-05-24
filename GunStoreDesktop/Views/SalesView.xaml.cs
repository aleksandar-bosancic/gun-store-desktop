using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Google.Protobuf.WellKnownTypes;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;
using GunStoreDesktop.Windows;
using MaterialDesignThemes.Wpf;

namespace GunStoreDesktop.Views;

public partial class SalesView : UserControl, INotifyPropertyChanged
{
    private readonly Employee? currentEmployee;
    private readonly List<Weapon> _weapons;
    private readonly ObservableCollection<Item> _items;
    private ObservableCollection<Item> _filteredItems;
    public ObservableCollection<Item> ReceiptItems { get; set; }
    public double TotalPrice { get; set; }
    private Buyer currentBuyer;

    public ObservableCollection<Item> FilteredItems
    {
        get => _filteredItems;
        set
        {
            if (_filteredItems != value)
            {
                _filteredItems = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public SalesView()
    {
        _items = new ObservableCollection<Item>(DataFactory.GetMySqlDataFactory().Item.getItems());
        _filteredItems = _items;
        FilteredItems = _items;
        _weapons = new List<Weapon>(DataFactory.GetMySqlDataFactory().Weapon.getWeapons());
        ReceiptItems = new ObservableCollection<Item>();
        currentBuyer = new Buyer();
        currentEmployee = (Application.Current.MainWindow as MainWindow)?.CurrentEmployee;
        InitializeComponent();
        DataContext = this;
    }

    private void Filter_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        FilteredItems = _items;
        string filter = (sender as TextBox)?.Text ?? "";
        FilteredItems = new ObservableCollection<Item>(FilteredItems.Where(item => item.Model.Contains(filter)
            || item.Manufacturer.Contains(filter)).ToList());
    }

    private void Row_OnClick(object sender, MouseButtonEventArgs e)
    {
        Console.WriteLine("item");
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (ListView.SelectedItem is Item item)
        {
            if (item.InStock == 1)
            {
                _items.Remove(item);
                FilteredItems.Remove(item);
            }

            ReceiptItems.Add(item);
            item.InStock = item.InStock - 1;
        }

        CollectionViewSource.GetDefaultView(FilteredItems).Refresh();
        updateTotalPrice();
    }

    private void ReceiptRow_OnClick(object sender, MouseButtonEventArgs e)
    {
        Console.WriteLine("receipt click");
    }

    private void ConfirmSaleButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (!ReceiptItems.Any())
        {
            MessageBox.Show($"{TranslationSource.Instance["ListEmptyText"]}", 
                $"{TranslationSource.Instance["ListEmptyCaption"]}" , MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        bool hasWeapons = ReceiptItems.Select(item => item.Id)
            .Intersect(_weapons.Select(weapon => weapon.ItemId))
            .Any();
        if (hasWeapons)
        {
            currentBuyer = new Buyer();
            BuyerFirearmPermitConfirmationWindow buyerFirearmPermitConfirmationWindow = new(currentBuyer);
            bool? dialogResult = buyerFirearmPermitConfirmationWindow.ShowDialog();
            if (dialogResult ?? false)
            {
                confirmSale();
            }
        }
        else
        {
            confirmSale();
        }
    }

    private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (ReceiptList.SelectedItem is Item item)
        {
            ReceiptItems.Remove(item);
            if (_items.Contains(item))
            {
                item.InStock = item.InStock + 1;
            }
            else
            {
                item.InStock = item.InStock + 1;
                _items.Add(item);
                FilteredItems = _items;
            }
        }

        CollectionViewSource.GetDefaultView(FilteredItems).Refresh();
        updateTotalPrice();
    }

    private void updateTotalPrice()
    {
        if (!ReceiptItems.Any())
        {
            TotalPrice = 0;
        }
        else
        {
            TotalPrice = ReceiptItems.Select(item => item.Price).Aggregate((d, d1) => d1 + d);
        }

        TotalPriceTextBox.Text = TotalPrice.ToString(CultureInfo.CurrentCulture);
    }

    private void confirmSale()
    {
        Receipt receipt = DataFactory.GetMySqlDataFactory().Receipt.saveReceipt(new Receipt()
        {
            Buyer = currentBuyer,
            Employee = currentEmployee,
            DateTime = DateTime.Now,
            TotalPrice = TotalPrice
        });
        var set = ReceiptItems.GroupBy(item => item.Id);
        foreach (var item in set)
        {
            DataFactory.GetMySqlDataFactory().ReceiptItem.saveReceiptItem(new ReceiptItem()
            {
                ItemId = item.Key,
                ReceiptId = receipt.Id,
                ItemPrice = getItemPrice(item.Key),
                Amount = item.Count()
            });
        }
        foreach (Item item in ReceiptItems.Distinct())
        {
            DataFactory.GetMySqlDataFactory().Item.updateInStockById(item.Id, item.InStock);
        }
        ReceiptItems.Clear();
        TotalPriceTextBox.Text = "0";
    }

    private double getItemPrice(int id)
    {
        return _items.First(item => item.Id == id).Price;
    }
}