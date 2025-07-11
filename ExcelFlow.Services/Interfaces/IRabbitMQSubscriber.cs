using System;

namespace ExcelFlow.Services.Interfaces;

public interface IRabbitMQSubscriber
{
    void Subscribe<T>(string queueName, Func<T, Task> handleMessage) where T : class;
}