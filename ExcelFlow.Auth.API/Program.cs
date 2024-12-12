using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.Repositories;
using ExcelFlow.Services.Interfaces;
using ExcelFlow.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add services to the container.
builder.Services.AddControllers();
// Repository'yi DI konteynerine kaydet
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));

// BaseService'i DI konteynerine kaydet
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

builder.Services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Enable routing for controllers
app.UseRouting();

// Map controllers to routes
app.MapControllers();

app.Run();

