using System.Text;
using StockMarket.StockMsgsProcessor.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockMarket.Common.Models;

namespace StockMarket.StockMsgsProcessor.Services
{
    public class RabbitMqProcessor
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IStockMessageProcessor _stockMessageProcessor;

        public RabbitMqProcessor(IConfiguration config, IStockMessageProcessor stockMessageProcessor)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = config.GetSection("RabbitConfig:HostName").Value
            };
            _stockMessageProcessor = stockMessageProcessor;
        }

        public void StartProcessing(string queueName) {
            var connection = _connectionFactory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var stockMsg = JsonConvert.DeserializeObject<RabbitStockMsg>(message);
                _stockMessageProcessor.ProcessStockMsg(stockMsg);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        } 
    }
}
