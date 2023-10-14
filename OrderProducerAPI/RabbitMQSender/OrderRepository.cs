using Mensagem.MessageBus;
using OrderProducerAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderProducerAPI
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public OrderRepository()
        {
            _hostName = "localhost";
            _password = "guest"; // Como não está em Ambîênte de produção, pode ficar default do RabbitMQ
            _userName = "guest"; // Como não está em Ambîênte de produção, pode ficar default do RabbitMQ
        }

        public void SendMessage(BaseMessage Message, string queueName)
        {
            // Criação da Connection Factoring
            var factory = new ConnectionFactory 
            { 
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
            byte[] body = GetMessageByteArray(Message);
            channel.BasicPublish(
                    exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

        private byte[] GetMessageByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Serializa as classes filhas
            };
            var json = JsonSerializer.Serialize<CellConcertOrder>((CellConcertOrder)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
