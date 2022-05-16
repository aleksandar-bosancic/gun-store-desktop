using System;

namespace GunStoreDesktop.Data.Model;

public class Ammunition
{
    public int Id { get; set; }
    public string? MunitionType { get; set; }
    public int PackageSize { get; set; }
    public string? Caliber { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Ammunition ammunition && Id == ammunition.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id.GetHashCode());
    }
}