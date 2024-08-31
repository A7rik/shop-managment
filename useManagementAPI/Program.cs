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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utils;
using Microsoft.OpenApi.Models;
using Middleware;
using Application.Services.Jwt;
using Domain.Models.Utils;

namespace useManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DataBase connection
            var dbSettings = builder.Configuration.GetSection("DatabaseSettings");
            var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectionString = connectionStringTemplate
                .Replace("{DBServer}", dbSettings["DBServer"])
                .Replace("{DBPort}", dbSettings["DBPort"])
                .Replace("{DBName}", dbSettings["DBName"])
                .Replace("{DBUser}", dbSettings["DBUser"])
                .Replace("{DBPassword}", dbSettings["DBPassword"]);
            builder.Services.Configure<DatabaseConnectionModel>(options =>
            {
                options.ConnectionString = connectionString;
            });

            // Register repositories
            builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Register services
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();

            // Register IPasswordHasher implementation
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

            // Register JWT service
            builder.Services.AddSingleton<IJwtService, JwtService>();

            // Configure JWT Authentication
            var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfigModel>();

            builder.Services.Configure<JwtConfigModel>(builder.Configuration.GetSection("JwtConfig"));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<TokenBlacklistMiddleware>();
            app.UseMiddleware<RoleBasedAuthorizationMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
