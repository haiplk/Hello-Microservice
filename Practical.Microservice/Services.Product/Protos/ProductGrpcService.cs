using Grpc.Core;
using Services.Product.Common;
using Services.Product.Models;
using Services.Product.Services.Commands;
using Services.Product.Services.Queries;

namespace Services.Product.Protos
{
    public class ProductGrpcService : ProductServiceGrpc.ProductServiceGrpcBase
    {
        private readonly IDispatcher dispatcher;

        public ProductGrpcService(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public override async Task<ListProductsResponse> GetAll(FilterProductRequest request, ServerCallContext context)
        {
            var query = new FilterProductQuery();
            var items = await dispatcher.DispatchAsync<List<ProductViewModel>>(query);

            var result = new ListProductsResponse();
            result.Products.AddRange(items.Select(p => new ProductResponse
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Price = (double)p.Price,
                Description = p.Description
            }).ToList());

            return result;
        }

        public override async Task<ProductResponse> AddProduct(UpsertProductRequest request, ServerCallContext context)
        {
            var command = new UpsertProductCommad
            {
                Price = (decimal)request.Price,
                Name = request.Name,
                Description = request.Description,

            };

            var id = await dispatcher.DispatchAsync<Guid>(command);
            return new ProductResponse
            {
                Id = id.ToString()
            };
        }
    }
}
