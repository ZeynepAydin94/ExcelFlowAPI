using ExcelFlow.Worker.Consumers;
using ExcelFlow.Web.Common.Extensions;
var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddDefaultAppSettings();
builder.Services.AddHostedService<ExcelMessageConsumer>();
builder.Services.AddExcelFlowServices(builder.Configuration);
var host = builder.Build();
host.Run();
