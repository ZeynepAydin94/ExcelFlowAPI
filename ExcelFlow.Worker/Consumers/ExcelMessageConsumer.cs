using System.Text;
using System.Text.Json;
using ExcelFlow.Core.Configurations;
using ExcelFlow.Core.Messages;
using ExcelFlow.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExcelFlow.Worker.Consumers;

public class ExcelMessageConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRabbitMQSubscriber _subscriber;
    public ExcelMessageConsumer(IServiceProvider serviceProvider, IRabbitMQSubscriber subscriber)
    {


        _serviceProvider = serviceProvider;
        _subscriber = subscriber;
    }


    override protected async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _subscriber.Subscribe<ExcelFileProcessMessage>("excel-processing", async message =>
         {
             using var scope = _serviceProvider.CreateScope();
             var processor = scope.ServiceProvider.GetRequiredService<IExcelProcessorService>();
             await processor.ProcessAsync(message.FileId);
         });

        // uygulama bitene kadar açık kalmalı
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}