using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Entities
{
    public class ShoppingCart
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
