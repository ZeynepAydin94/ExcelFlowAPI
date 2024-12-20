using System.Net;
using System.Net.Security;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.DbContexts;
using ExcelFlow.DataAccess.Repositories;
using ExcelFlow.Services.Interfaces;
using ExcelFlow.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Add services to the container.
builder.Services.AddControllers();
// Repository'yi DI konteynerine kaydet
// Register DbContext with the DI container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Replace with your actual connection string


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));

// BaseService'i DI konteynerine kaydet
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();
app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
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
static void Main()
{
    // Sertifika doğrulama callback fonksiyonunu ayarla
    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) =>
    {
        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true; // Sertifika geçerli
        }

        // Hata durumunda detayları logla
        Console.WriteLine($"Sertifika hatası: {sslPolicyErrors}");
        return false; // Sertifika hatalı
    });

    // Burada SSL bağlantısı kurmaya çalışabilirsiniz, örneğin:
    var request = System.Net.HttpWebRequest.Create("https://example.com");
    try
    {
        var response = request.GetResponse();
        Console.WriteLine("Bağlantı başarılı.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata: {ex.Message}");
    }
}
