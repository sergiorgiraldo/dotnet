using Microsoft.EntityFrameworkCore;
using MyStuff.Models;

namespace MyStuff.Data
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
