using System;

namespace GunStoreDesktop.Data.Model;

public class FirearmPermit
{
    public string Uid { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly ExpirationDate { get; set; }
}