using BugStore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cnnString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(cnnString));

builder.Services.AddScoped<BugStore.Handlers.Customers.Handler>();
builder.Services.AddScoped<BugStore.Handlers.Products.Handler>();
builder.Services.AddScoped<BugStore.Handlers.Orders.Handler>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
