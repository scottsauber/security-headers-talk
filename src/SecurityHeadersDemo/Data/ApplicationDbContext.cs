using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityHeadersDemo.Data.Models;

namespace SecurityHeadersDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items{ get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
