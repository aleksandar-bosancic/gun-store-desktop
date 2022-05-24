using System;

namespace GunStoreDesktop.Data.Model;

public class Buyer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public FirearmPermit? FirearmPermit { get; set; }
}