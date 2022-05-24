using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class DataFactoryMySql : DataFactory
{
    private EmployeeMySql? _employeeMySql;
    private ItemMySql? _itemMySql;
    private BuyerMySql? _buyerMySql;
    private WeaponMySql? _weaponMySql;
    private FirearmPermitMySql? _firearmPermitMySql;
    private ReceiptMySql? _receiptMySql;
    private ReceiptItemMySql? _receiptItemMySql;
    private EquipmentMySql? _equipmentMySql;
    private AmmunitionMySql? _ammunitionMySql;
    private SupplierMySql? _supplierMySql;
    private PurchaseOrderMySql? _purchaseOrderMySql;
    private ItemPurchaseOrderMySql? _itemPurchaseOrderMySql;
    

    public override IEmployee Employee => _employeeMySql ?? new EmployeeMySql();
    public override IItem Item => _itemMySql ?? new ItemMySql();
    public override IBuyer Buyer => _buyerMySql ?? new BuyerMySql();
    public override IWeapon Weapon => _weaponMySql ?? new WeaponMySql();
    public override IFirearmPermit FirearmPermit => _firearmPermitMySql ?? new FirearmPermitMySql();
    public override IReceipt Receipt => _receiptMySql ?? new ReceiptMySql();
    public override IReceiptItem ReceiptItem => _receiptItemMySql ?? new ReceiptItemMySql();
    public override IEquipment Equipment => _equipmentMySql ?? new EquipmentMySql();
    public override IAmmunition Ammunition => _ammunitionMySql ?? new AmmunitionMySql();
    public override ISupplier Supplier => _supplierMySql ?? new SupplierMySql();
    public override IPurchaseOrder PurchaseOrder => _purchaseOrderMySql ?? new PurchaseOrderMySql();
    public override IItemPurchaseOrder ItemPurchaseOrder => _itemPurchaseOrderMySql ?? new ItemPurchaseOrderMySql();
}