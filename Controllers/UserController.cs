using System;
using System.Collections.Generic;
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
    public class UserController : Controller
    {

        private readonly IShoppingContext context;

        public UserController(IShoppingContext _context)
        {
            context = _context;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(long id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return BadRequest("No user with that ID");
            return Ok(user);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<long>> Post([FromBody]User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(user.Id);
        }

    }
}
