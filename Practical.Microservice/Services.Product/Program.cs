using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.Product.Common;
using Services.Product.Common.Commands;
using Services.Product.Common.Queries;
using Services.Product.Data.DbContexts;
using Services.Product.Models;
using Services.Product.Protos;
using Services.Product.Services.Commands;
using Services.Product.Services.Queries;

namespace Services.Product
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddGrpc().AddJsonTranscoding();
            builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));

            builder.Services.AddGrpcSwagger();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo { Title = "Product gRPC transcoding", Version = "v1" });
            });



            // Use master-slave db but point to 1 db for example
            builder.Services.AddDbContext<ProductMasterDbContext>(opt => opt.UseInMemoryDatabase("ProductDb_Master"));
            builder.Services.AddDbContext<ProductSlaveDbContext>(opt => opt.UseInMemoryDatabase("ProductDb_Master"));

            // CQRS
            builder.Services.AddScoped<IDispatcher, Dispatcher>();

            // TODO: Should implement dynamic resolver
            builder.Services.AddTransient<IQueryHandler<FilterProductQuery, List<ProductViewModel>>, FilterProductQueryHandler>();
            builder.Services.AddTransient<ICommandHandler<UpsertProductCommad, Guid>, UpsertProductCommandHandler>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            app.MapGrpcService<ProductGrpcService>();

            app.Run();
        }
    }
}