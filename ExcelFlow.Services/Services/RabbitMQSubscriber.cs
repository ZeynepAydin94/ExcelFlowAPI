using System;
using System.Text;
using System.Text.Json;
using ExcelFlow.Core.Configurations;
using ExcelFlow.Services.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExcelFlow.Services.Services;

public class RabbitMQSubscriber : IRabbitMQSubscriber

{
    private readonly RabbitMqSettings _settings;
    private readonly IConnection _connection;

    public RabbitMQSubscriber(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;

        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
    }

    public void Subscribe<T>(string queueName, Func<T, Task> handleMessage) where T : class
    {
        var channel = _connection.CreateModel();
        channel.QueueDeclare(queueName, true, false, false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (sender, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<T>(json);
                if (message != null)
                    await handleMessage(message);

                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RabbitMQSubscriber] Error: {ex.Message}");
                channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }
}
