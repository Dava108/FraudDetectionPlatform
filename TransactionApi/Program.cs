using Microsoft.EntityFrameworkCore;
using TransactionApi.Data;
using TransactionApi.Services;
using StackExchange.Redis;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IRedisService, RedisService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration["Redis:ConnectionString"]!
    )
);
var app = builder.Build();

app.MapControllers();

app.Run();