using StockMarket.StockMsgsProcessor.Models;

namespace StockMarket.StockMsgsProcessor.Services.Interfaces
{
    public interface IStockMessageProcessor
    {
        void ProcessStockMsg(RabbitStockMsg stockmsg);
    }
}
