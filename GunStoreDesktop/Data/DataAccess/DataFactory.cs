namespace GunStoreDesktop.Data.DataAccess;

public abstract class DataFactory
{
    public abstract IEmployee Employee { get; }
    public abstract IItem Item { get; }
    public abstract IBuyer Buyer { get; }
    public abstract IWeapon Weapon { get; }
    public abstract IFirearmPermit FirearmPermit { get; }
    public abstract IReceipt Receipt { get; }
    public abstract IReceiptItem ReceiptItem { get; }
    public abstract IEquipment Equipment { get; }
    public abstract IAmmunition Ammunition { get; }
    public abstract ISupplier Supplier { get; }
    public abstract IPurchaseOrder PurchaseOrder { get; }
    public abstract IItemPurchaseOrder ItemPurchaseOrder { get; }

    public static DataFactory GetMySqlDataFactory()
    {
        return new MySql.DataFactoryMySql();
    }
}