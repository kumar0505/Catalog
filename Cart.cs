namespace Catalog;


public class Cart
{
    public int  Id { get; set; } = 0;
    public int  ConsumerId { get; set; } = 0;
    public decimal TotalAmount { get; set; } = 0;
    public int OrderId { get; set; } = 0;
    public DateTimeOffset EntryTime { get; set; } = DateTimeOffset.UtcNow;

}


public class CartItem
{
    public int  Id { get; set; } = 0;
    public int  CartId { get; set; } = 0;
    public decimal ProductId { get; set; } = 0;
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; } = 0;
    public DateTimeOffset EntryTime { get; set; } = DateTimeOffset.UtcNow;
}

public class Order 
{
    public int  Id { get; set; } = 0;
    public int  ConsumerId { get; set; } = 0;
    public string ProductId { get; set; } = "P";
    public decimal TotalAmount { get; set; } = 0;
    public int ItemCount  { get; set; } = 0;
    public DateTimeOffset EntryTime { get; set; } = DateTimeOffset.UtcNow;
}


public class OrderData 
{
    public int  Id { get; set; } = 0;
    public int  OrderId { get; set; } = 0;
    public int Quantity  { get; set; } = 0;
    public decimal Price { get; set; } = 0;
}


