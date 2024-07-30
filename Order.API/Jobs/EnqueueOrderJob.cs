using System.Text;
using System.Text.Json;
using Order.API.Mappings;
using Order.API.Models.Output;
using Order.Messages.Events;
using RabbitMQ.Client;

namespace Order.API.Jobs;

public class EnqueueOrderJob
{
    private readonly IConfiguration _configuration;

    public EnqueueOrderJob(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task Execute(OrderResponse orderResponse, CancellationToken ct)
    {
        // Do something
        
        var factory = new ConnectionFactory { HostName = _configuration.GetValue<string>("RabbitMQ:Host") };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: "order_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(CreateOrderToOrderMapping.FromEntity(orderResponse)));
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        
        channel.BasicPublish(exchange: string.Empty,
            routingKey: "order_queue",
            basicProperties: properties,
            body: body);
        
        return Task.CompletedTask;
    }
}