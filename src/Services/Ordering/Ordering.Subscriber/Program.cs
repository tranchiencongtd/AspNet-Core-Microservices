// See https://aka.ms/new-console-template for more information


using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var conntionFactory = new ConnectionFactory
{
  HostName= "localhost",
};

var connection = conntionFactory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("orders", exclusive: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (_, eventArgs) =>
{
  var body = eventArgs.Body.ToArray();
  var message = Encoding.UTF8.GetString(body);

  Console.WriteLine(message);
};

channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
Console.ReadKey();

