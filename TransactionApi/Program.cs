using Microsoft.EntityFrameworkCore;
using TransactionApi.Data;
using TransactionApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
var app = builder.Build();

app.MapControllers();

app.Run();