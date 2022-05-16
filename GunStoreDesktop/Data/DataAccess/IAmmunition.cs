using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IAmmunition
{
    List<Ammunition> getAmmunition();
    void saveAmmunition();
    void deleteAmmunitionById();
}