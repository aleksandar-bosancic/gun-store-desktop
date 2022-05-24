using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IPurchaseOrder
{
    PurchaseOrder save(PurchaseOrder purchaseOrder);
}