using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IBuyer
{
    List<Buyer> getBuyers();
    void saveBuyer(Buyer buyer);
}