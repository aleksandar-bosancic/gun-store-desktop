namespace GunStoreDesktop.Data.Model;

public class Item
{
    public int Id { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
}