using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IFirearmPermit
{
    List<FirearmPermit> getFirearmPermits();

    FirearmPermit? findByUid(string uid);
}