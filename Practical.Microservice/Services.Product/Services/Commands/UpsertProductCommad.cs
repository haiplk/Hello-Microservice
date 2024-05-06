
using Microsoft.EntityFrameworkCore;
using Services.Product.Common.Commands;
using Services.Product.Data.DbContexts;
using Services.Product.Models;
using System.Xml.Linq;

namespace Services.Product.Services.Commands
{
    public class UpsertProductCommad : ICommand
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTime? CreatedDated { get; set; }

        public Guid? CreatedUserId { get; set; }

        public UserInfoViewModel? CreatedUser { get; set; }
    }


    public class UpsertProductCommandHandler : ICommandHandler<UpsertProductCommad, Guid>
    {
        private readonly ProductMasterDbContext productMasterDbContext;

        public UpsertProductCommandHandler(ProductMasterDbContext productMasterDbContext)
        {
            this.productMasterDbContext = productMasterDbContext;
        }

        public async Task<Guid> HandleAsync(UpsertProductCommad command, CancellationToken cancellationToken = default)
        {
            var id = command.Id.GetValueOrDefault();
            var current = command.Id.HasValue ? await productMasterDbContext.Products.FirstOrDefaultAsync(p => p.Id == command.Id) : null;
            if (current == null)
            {
                id = Guid.NewGuid();
                productMasterDbContext.Products.Add(new Data.Entities.Product
                {
                    Id = id,
                    Name = command.Name,
                    Description = command.Description,
                    CreatedUserId = command.CreatedUserId,
                    CreatedDated = command.CreatedDated,
                    Price = command.Price
                });
            }
            else
            {
                current.Name = command.Name;
                current.Description = command.Description;
                current.CreatedUserId = command.CreatedUserId;
                current.CreatedDated = command.CreatedDated;
                current.Price = command.Price;
            }

            await productMasterDbContext.SaveChangesAsync();

            return id;
        }
    }
}
