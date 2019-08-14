using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Entities
{
    public interface IShoppingContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}