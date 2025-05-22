using System.Net;
using System.Net.Security;
using System.Text;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.DbContexts;
using ExcelFlow.DataAccess.Repositories;
using ExcelFlow.Services.Interfaces;
using ExcelFlow.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])
            )
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable routing for controllers
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
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
