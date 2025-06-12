using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using ExcelFlow.Core.Messages;
using ExcelFlow.Services.Interfaces;
using ExcelFlow.Core.Configurations;
using Microsoft.Extensions.Options;

namespace ExcelFlow.Services.Implementations;

public class RabbitMQPublisherService : IRabbitMQPublisherService
{

    private readonly RabbitMqSettings _settings;

    public RabbitMQPublisherService(IOptions<RabbitMqSettings> options)
    {
        _settings = options.Value;
    }
    public void PublishExcelProcessMessage(ExcelFileProcessMessage message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        using var connection = factory.CreateConnection();   // ✔️ .NET 9 ile uyumlu
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "excel-processing",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "",
                             routingKey: "excel-processing",
                             basicProperties: null,
                             body: body);

        Console.WriteLine("📤 Mesaj kuyruğa gönderildi.");
    }
}