using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IItem
{
    List<Item> getItems();
    void updateInStockById(int id, int inStock);
    Item saveItem(Item item);
}