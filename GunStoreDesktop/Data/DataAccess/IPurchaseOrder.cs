using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IPurchaseOrder
{
    void save(PurchaseOrder purchaseOrder);
}