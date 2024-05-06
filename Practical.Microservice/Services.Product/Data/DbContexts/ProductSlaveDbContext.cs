using Microsoft.EntityFrameworkCore;

namespace Services.Product.Data.DbContexts
{
    public class ProductSlaveDbContext : DbContext
    {
        public ProductSlaveDbContext(DbContextOptions<ProductSlaveDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Product> Products { get; set; }
    }
}
