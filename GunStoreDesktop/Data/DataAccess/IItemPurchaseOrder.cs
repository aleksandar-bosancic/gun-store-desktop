using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IItemPurchaseOrder
{
    void save(ItemPurchaseOrder itemPurchaseOrder);
}