using StockMarket.Common.Models;

namespace StockMarket.StockMsgsProcessor.Services.Interfaces
{
    public interface IStockMessageProcessor
    {
        void ProcessStockMsg(RabbitStockMsg stockmsg);
    }
}
