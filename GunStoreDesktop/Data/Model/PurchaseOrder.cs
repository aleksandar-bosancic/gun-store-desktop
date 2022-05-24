using System;

namespace GunStoreDesktop.Data.Model;

public class PurchaseOrder
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public DateTime DateTime { get; set; }
}