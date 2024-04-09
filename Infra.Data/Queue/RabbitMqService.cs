using Application.Interfaces;
using Infra.Data.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infra.Data.Queue
{
    public class RabbitMqService : IQueueService, IDisposable
    {
        private readonly IOptions<RabbitMqConfiguration> options;
        private readonly IModel _model;
        private readonly IConnection _connection;

        const string _queueName = "payment";
        const string _exchangeName = "store.exchange";
        public RabbitMqService(IOptions<RabbitMqConfiguration> options, ILoggerFactory loggerFactory)
        {
            this.options = options;
            _connection = CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Fanout, durable: true, autoDelete: false);
            _model.QueueBind(_queueName, _exchangeName, string.Empty);
        }
        public async Task ReadMessages(Func<string, Task> action)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var text = System.Text.Encoding.UTF8.GetString(body);
                await action(text);
                _model.BasicAck(ea.DeliveryTag, false);
            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        public async Task SendMessage(string message)
        {
            using var model = _connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(message);
            model.BasicPublish(_exchangeName,
                                 string.Empty,
                                 basicProperties: null,
                                 body: body);
            await Task.CompletedTask;
        }

        private IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                UserName = options.Value.Username,
                Password = options.Value.Password,
                HostName = options.Value.HostName
            };
          
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
