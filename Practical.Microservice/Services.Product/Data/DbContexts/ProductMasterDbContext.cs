using Microsoft.EntityFrameworkCore;

namespace Services.Product.Data.DbContexts
{
    public class ProductMasterDbContext : DbContext
    {
        public ProductMasterDbContext(DbContextOptions<ProductMasterDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Product> Products { get; set; }
    }
}
