using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserManagement.Database.DbContexts;
using UserManagement.Database.Interfaces;
using UserManagement.Database.Repositories;
using UserManagement.Protos;
using UserManagement.Services;
using UserManagement.Services.Impls;

namespace UserManagement
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
                    new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
            });


            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            // Use master-slave db but point to 1 db for example
            builder.Services.AddDbContext<UserMasterDbContext>(opt => opt.UseInMemoryDatabase("UserDb_Master"));
            builder.Services.AddDbContext<UserSlaveDbContext>(opt => opt.UseInMemoryDatabase("UserDb_Master"));

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
            app.MapGrpcService<UserGrpcService>();

            app.Run();
        }
    }
}