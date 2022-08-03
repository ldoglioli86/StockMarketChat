using System.Text;
using StockMarket.Chat.Models;
using StockMarket.Chat.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace StockMarket.Chat.Services
{
    public class RabbitStockMsgProducerService : IRabbitStockMsgProducerService
    {
        private readonly IConfiguration _config;

        public RabbitStockMsgProducerService(IConfiguration config)
        {
            _config = config;
        }

        public void SendStockMessage(RabbitStockMsg stockMsg)
        {
            var queueName = _config.GetSection("RabbitConfig:StockQueue").Value;
            var factory = new ConnectionFactory
            {
                HostName = _config.GetSection("RabbitConfig:HostName").Value
            };
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, exclusive: false);
            var json = JsonConvert.SerializeObject(stockMsg);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
        }
    }
}
