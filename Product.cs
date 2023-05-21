namespace Catalog;

public class Product  
{
    public int  Id { get; set; } = 0;
    public decimal Price { get; set; }
    public string Name { get; set; }  = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal InKg { get; set; } = 1;
    public int Quantity  { get; set; } = 0;

    public DateTimeOffset EntryTime { get; set; } = DateTimeOffset.UtcNow;
}
