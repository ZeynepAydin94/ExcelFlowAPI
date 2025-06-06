

using ExcelFlow.Web.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Servis yapılandırmaları
builder.Services.AddControllers();
builder.Services.AddExcelFlowServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerWithJwtSupport();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

// CORS
builder.Services.AddCors();

// App
var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS, auth, routing
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();