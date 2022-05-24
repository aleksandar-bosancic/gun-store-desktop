namespace GunStoreDesktop.Data.Model;

public class ReceiptItem
{
    public int ReceiptId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
    public double ItemPrice { get; set; }
}