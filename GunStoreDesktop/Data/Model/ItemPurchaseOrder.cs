namespace GunStoreDesktop.Data.Model;

public class ItemPurchaseOrder
{
    public int ItemId { get; set; }
    public int PurchaseOrderId { get; set; }
    public int Amount { get; set; }
    public double SupplierPrice { get; set; }
}