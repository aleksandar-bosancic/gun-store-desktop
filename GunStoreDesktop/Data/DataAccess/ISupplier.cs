using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface ISupplier
{
    List<Supplier> getSuppliers();
}