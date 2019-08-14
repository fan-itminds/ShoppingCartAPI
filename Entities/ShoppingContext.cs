using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Entities
{
    public class ShoppingContext : DbContext, IShoppingContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options)
           : base(options)
        {
        }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
