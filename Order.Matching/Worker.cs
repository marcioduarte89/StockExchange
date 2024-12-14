using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Order.Matching;

public class Worker : BackgroundService
{
    private IConfiguration _configuration { get; set; }
    
    public Worker(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "order_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");

            // here channel could also be accessed as ((EventingBasicConsumer)sender).Model
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            channel.BasicConsume(queue: "order_queue",
                autoAck: false,
                consumer: consumer);

            await Task.Delay(1000, stoppingToken);
        }
    }
}