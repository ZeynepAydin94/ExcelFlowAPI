using System.Text;
using System.Text.Json;
using ExcelFlow.Core.Configurations;
using ExcelFlow.Core.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExcelFlow.Worker.Consumers;

public class ExcelMessageConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly RabbitMqSettings _settings;
    public ExcelMessageConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "excel-processing",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());

            using var scope = _serviceProvider.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IExcelProcessorService>();

            try
            {
                var message = JsonSerializer.Deserialize<ExcelFileProcessMessage>(json);
                await processor.ProcessAsync(message!);
                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                channel.BasicNack(ea.DeliveryTag, false, requeue: true);
            }
        };

        channel.BasicConsume(queue: "excel-processing", autoAck: false, consumer: consumer);
        return Task.CompletedTask;
    }
}