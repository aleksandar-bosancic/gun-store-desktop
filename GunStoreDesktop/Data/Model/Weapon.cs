namespace GunStoreDesktop.Data.Model;

public class Weapon
{
    public int ItemId { get; set; }
    public string Caliber { get; set; }
    public int MagazineCapacity { get; set; }
    public bool IsFirearm { get; set; }
}