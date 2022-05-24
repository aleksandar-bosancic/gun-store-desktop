using System;

namespace GunStoreDesktop.Data.Model;

public class Receipt
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public double TotalPrice { get; set; }
    public Employee Employee { get; set; }
    public Buyer? Buyer { get; set; }
}