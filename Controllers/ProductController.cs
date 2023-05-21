using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase 
{
    private static List<Product> products = new List<Product>
        {
        };

    [HttpGet]
    public async Task<ActionResult<List<Product>>> Get() 
    {
        return Ok(products);
    }

    [HttpGet("*{id}*")]
    public async Task<ActionResult<Product>> Get(int id) 
    {
        var product = products.Find(prod => prod.Id == id);
        if (product == null){
            return BadRequest("Product not found");
        }
        else {
            return Ok(product);
        }
    }

    [HttpPost]
    public async Task<ActionResult<List<Product>>> addProduct(Product prod) 
    {
        prod.Id = products.Count + 1;
        prod.EntryTime = DateTimeOffset.UtcNow;
        products.Add(prod);
        return Ok(products);        
    }


    [HttpPut]
    public async Task<ActionResult<List<Product>>> UpdateProduct(Product prod) 
    {
        var product = products.Find( item => item.Id == prod.Id);
        if (product == null){
            return BadRequest("Product not found");
        }
        else {
            product.Name = prod.Name;
            product.Price = prod.Price;
            product.Quantity = product.Quantity;
            return Ok(products);        
        }
    }


    [HttpDelete("*{id}*")]
    public async Task<ActionResult<Product>> Delete(int id) 
    {
        var product = products.Find( item => item.Id == id);
        if (product != null){
            return BadRequest("Product not exist");
        }
        else {
            products.Remove(product);
            return Ok("Deleted sucessfully.");        
        }
    }
}