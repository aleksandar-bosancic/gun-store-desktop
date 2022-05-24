using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GunStoreDesktop.Data.DataAccess;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Util;

namespace GunStoreDesktop.Windows;

public partial class BuyerFirearmPermitConfirmationWindow : Window
{
    public Buyer Buyer { get; set; }
    public FirearmPermit FirearmPermit { get; set; }
    private readonly List<FirearmPermit> _permits;
    public string DateOfBirth { get; set; }
    private bool _permitExists = false;

    public BuyerFirearmPermitConfirmationWindow(Buyer buyer)
    {
        Buyer = buyer;
        FirearmPermit = new FirearmPermit();
        _permits = DataFactory.GetMySqlDataFactory().FirearmPermit.getFirearmPermits();
        InitializeComponent();
        DataContext = this;
    }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        FirearmPermit? permit = _permits.Find(permit => permit.Uid.Equals(FirearmPermit.Uid));
        if (permit is not null)
        {
            FirearmPermit = permit;
            Buyer.FirearmPermit = permit;
            ExpirationDate.Visibility = Visibility.Visible;
            ExpirationDate.Text = FirearmPermit.ExpirationDate.ToString();
            _permitExists = true;
        }
        else
        {
            MessageBox.Show(this, $"{TranslationSource.Instance["PermitNotFoundText"]}", 
                $"{TranslationSource.Instance["PermitNotFoundText"]}" , MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
    {
        List<Buyer> buyers = DataFactory.GetMySqlDataFactory().Buyer.getBuyers();
        if (!_permitExists)
        {
            DialogResult = false;
            MessageBox.Show(this, $"{TranslationSource.Instance["PermitNotFoundText"]}", 
                $"{TranslationSource.Instance["PermitNotFoundText"]}" , MessageBoxButton.OK, MessageBoxImage.Warning);
            Close();
            return;
        }
        if (Buyer.Name == string.Empty || Buyer.LastName == string.Empty || DateOfBirth == string.Empty)
        {
            MessageBox.Show(this, $"{TranslationSource.Instance["FieldsEmptyText"]}", 
                $"{TranslationSource.Instance["FieldsEmptyText"]}" , MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        else
        {
            Buyer? tempBuyer = buyers.Find(buyer => buyer.Id.Equals(Buyer.Id));
            if (tempBuyer != null && tempBuyer.FirearmPermit != null && tempBuyer.FirearmPermit.Uid.Equals(Buyer.FirearmPermit!.Uid))
            {
                Buyer = tempBuyer;
            }
            else if (tempBuyer is null || !tempBuyer.Id.Equals(Buyer.Id))
            {
                DialogResult = false;
                MessageBox.Show(this, $"{TranslationSource.Instance["PermitNotFoundText"]}", 
                    $"{TranslationSource.Instance["PermitNotFoundText"]}" , MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
            else
            {   
                Buyer.DateOfBirth = DateOnly.Parse(DateOfBirth);
                DataFactory.GetMySqlDataFactory().Buyer.saveBuyer(Buyer);
            }
            DialogResult = true;
            Close();
        }
    }
}