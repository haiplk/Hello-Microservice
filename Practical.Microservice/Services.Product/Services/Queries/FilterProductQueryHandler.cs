using Microsoft.EntityFrameworkCore;
using Services.Product.Common.Queries;
using Services.Product.Data.DbContexts;
using Services.Product.Models;

namespace Services.Product.Services.Queries
{
    public class FilterProductQuery : IQuery
    {
        public string? Search { get; set; }
    }

    public class FilterProductQueryHandler : IQueryHandler<FilterProductQuery, List<ProductViewModel>>
    {
        private readonly ProductSlaveDbContext productSlaveDbContext;

        public FilterProductQueryHandler(ProductSlaveDbContext productSlaveDbContext)
        {
            this.productSlaveDbContext = productSlaveDbContext;
        }

        public async Task<List<ProductViewModel>> HandleAsync(FilterProductQuery query, CancellationToken cancellationToken = default)
        {
            var result = await productSlaveDbContext.Products.Where(p => p.Name.Contains(query.Search)).ToListAsync();

            return result.Select(p => new ProductViewModel
            {
                 Id = p.Id,
                 Name = p.Name,
                 Price = p.Price,
                 Description = p.Description,
                 CreatedUserId = p.CreatedUserId,
                 CreatedDated = p.CreatedDated
            }).ToList();
        }
    }
}
