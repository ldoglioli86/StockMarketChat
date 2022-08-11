using StockMarket.Common.Models;

namespace StockMarket.Chat.Services.Interfaces
{
    public interface IRabbitStockMsgProducerService
    {
        public void SendStockMessage(RabbitStockMsg stockMsg);
    }
}
