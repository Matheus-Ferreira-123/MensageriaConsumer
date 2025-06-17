using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "fila1",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer1 = new EventingBasicConsumer(channel);
consumer1.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Recebido: {message}");
};

channel.BasicConsume(
    queue: "fila1",
    autoAck: true,
    consumer: consumer1);

Console.WriteLine("Aguardando mensagens...");
Console.ReadLine();
