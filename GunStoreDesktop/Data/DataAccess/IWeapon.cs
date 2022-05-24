using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IWeapon
{
    List<Weapon> getWeapons();
    void saveWeapon(Weapon weapon);
}