using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IReceiptItem
{
    List<ReceiptItem> getReceiptItems();
    void saveReceiptItem(ReceiptItem receiptItem);
}