using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IEquipment
{
    List<Equipment> getEquipment();
    Equipment saveEquipment(Equipment equipment);
}