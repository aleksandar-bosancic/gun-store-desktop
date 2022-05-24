using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IReceipt
{
    List<Receipt> getReceipts();
    Receipt saveReceipt(Receipt receipt);
}