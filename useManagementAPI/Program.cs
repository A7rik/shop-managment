using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
app.UseMiddleware<GlobalExceptionMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();
