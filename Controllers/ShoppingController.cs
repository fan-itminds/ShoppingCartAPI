using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : Controller
    {
        private readonly IShoppingContext context;

        public ShoppingController(IShoppingContext _context)
        {
            context = _context;
        }

        // GET: get items from cart : api/Shopping/{id}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetShoppingCartItems(long userId)
        {
            var shoppingCart = await context.ShoppingCarts
                .Include(sc => sc.Items)
                .Where(sc => sc.UserId == userId)
                .FirstOrDefaultAsync();
            Debug.WriteLine(shoppingCart); // think missing to take items with on select.

            if (shoppingCart == null)
            {
                return NotFound();
            }

            return Ok(shoppingCart.Items);
        }

        //POST: add item to cart : api/Shopping/{userId}
        [HttpPost("{userId}")]
        public async Task<ActionResult<long>> PostShoppingCartItem(long userId, [FromBody] Item item)
        {
            ShoppingCart shoppingCart = await context.ShoppingCarts
                .Where(sc => sc.UserId == userId)
                .FirstOrDefaultAsync();
            

            //If user does not have shopping cart, create one
            if (shoppingCart == null)
            {
                User user = await context.Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();
                if (user == null) return BadRequest("User does not exist");
                shoppingCart = new ShoppingCart
                {
                    UserId = user.Id,
                    User = user,
                    Items = new Collection<Item>()
                };
                await context.ShoppingCarts.AddAsync(shoppingCart);
            }
            shoppingCart.Items.Add(item);
            await context.SaveChangesAsync();
            return item.Id;        
        } 

        //DELETE: remove item from cart : api/Item/{id}
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteShoppingCartItem(long itemId)
        {
            var item = await context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
            if(item == null)
            {
                return BadRequest("No item with that ID");
            }
            var cart = context.Items.Remove(item);
            await context.SaveChangesAsync();
            return Ok();
            
        }
    }
}
