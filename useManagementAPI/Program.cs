using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Infrastructure.Repository.Category;
using Infrastructure.Repository.Product;
using Infrastructure.Repository.User;
using Application.Services.Category;
using Application.Services.Product;
using Application.Services.User;

namespace useManagementAPI
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            // Get database settings from appsettings
            var dbSettings = builder.Configuration.GetSection("DatabaseSettings");
            var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

            // Replace placeholders with actual values
            var connectionString = connectionStringTemplate
                .Replace("{DBServer}", dbSettings["DBServer"])
                .Replace("{DBPort}", dbSettings["DBPort"])
                .Replace("{DBName}", dbSettings["DBName"])
                .Replace("{DBUser}", dbSettings["DBUser"])
                .Replace("{DBPassword}", dbSettings["DBPassword"]);

            // Configure DatabaseConnectionModel with the connection string
            builder.Services.Configure<DatabaseConnectionModel>(options =>
            {
                options.ConnectionString = connectionString;
            });
            //builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
            //builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            //builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            //builder.Services.AddScoped<IUsersRepository, UsersRepository>();


            // Register repositories
            builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Register services
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();




            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddSingleton<IMySingletonService, MySingletonService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseRouting();
            app.UseMiddleware<GlobalExceptionMiddleware>(); 
            //app.UseEndpoints();


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}
