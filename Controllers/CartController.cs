using Microsoft.AspNetCore.Mvc;
namespace Catalog.Controllers.CartController;

[Route("api/[controller]")]
[ApiController]

public class CartController : ControllerBase 
{
    private static List<Cart> carts = new List<Cart> {};

    private static List<CartItem> carts_items = new List<CartItem>{};

    private static List<Order> orders = new List<Order> {};

    private static List<OrderData> orders_data = new List<OrderData>{};

    [HttpPut("additem")]
    public async Task<ActionResult<List<CartItem>>> AddItem(Item item) 
    {   
        var cart = carts.Find(c => c.ConsumerId == item.ConsumerId &&  c.OrderId == 0);
        if (cart != null){
            var carts_item = carts_items.Find(pd => pd.CartId == cart.Id && pd.ProductId == item.ProductId);
            if (carts_item != null) {
                carts_item.Quantity += 1; 
            } else {
                CartItem new_item = new CartItem{
                    Id = carts_items.Count + 1,
                    CartId = cart.Id,
                    ProductId = item.ProductId,
                    Quantity = 1,
                    Price = item.Price,
                    EntryTime = DateTimeOffset.UtcNow,
                };
                carts_items.Add(new_item);
                carts_item = new_item;
            }
            cart.TotalAmount = item.Price;
            return Ok(carts_items.FindAll(pd => pd.CartId == cart.Id)); 
        } else {
                Cart new_cart = new Cart{
                    Id = carts.Count + 1,
                    ConsumerId = item.ConsumerId,
                    TotalAmount = item.Price,
                    OrderId  = 0,
                    EntryTime = DateTimeOffset.UtcNow,
                };
                carts.Add(new_cart);
                CartItem new_item = new CartItem{
                    Id = carts_items.Count + 1,
                    CartId = new_cart.Id,
                    ProductId = item.ProductId,
                    Quantity = 1,
                    Price = item.Price,
                    EntryTime = DateTimeOffset.UtcNow,
                };
                carts_items.Add(new_item);
                return Ok(carts_items.FindAll(pd => pd.CartId == new_cart.Id)); 
        }
    }
    
    [HttpPost("checkout")]
    public async Task<ActionResult<List<OrderData>>> PlaceOrder(int cart_id) 
    {
        var cart = carts.Find(c => c.Id == cart_id &&  c.OrderId == 0);
        if (cart != null){ 
            return BadRequest("Cart Not Found");
        } else {
                List<CartItem> items =  carts_items.FindAll(pd => pd.CartId == cart_id); 
                Order new_order = new Order{
                    Id = orders.Count + 1,
                    ConsumerId = cart.ConsumerId,
                    TotalAmount = cart.TotalAmount,
                    Status = "P",
                    ItemCount = items.Count,
                    EntryTime = DateTimeOffset.UtcNow,
                };

                orders.Add(new_order);
                cart.OrderId = new_order.Id;
                
                for (int i = 0; i < items.Count; i++) {
                    CartItem item = items[i];
                    OrderData new_item = new OrderData {
                        Id = orders_data.Count + 1,
                        OrderId = new_order.Id,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        EntryTime = DateTimeOffset.UtcNow,
                    };
                    orders_data.Add(new_item);
                }
                return Ok(orders_data.FindAll(pd => pd.OrderId == new_order.Id));
        }
    }
}