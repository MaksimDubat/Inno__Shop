
using RabbitMQ.Client;
using System.Text;

namespace ProductServiceAPI.RabbitMQconfig
{
    /// <summary>
    /// Класс для интеграции с RabbitMQ.
    /// </summary>
    public class RabbitMqService
    {
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqService(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"],
                Port = int.Parse(configuration["RabbitMQ:Port"]),
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublishMessage(string queueName, string message)
        {
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body); 
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }

        
    }
}
