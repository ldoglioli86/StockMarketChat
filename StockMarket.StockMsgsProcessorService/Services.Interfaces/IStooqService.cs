using StockMarket.StockMsgsProcessorService.Models;

namespace StockMarket.StockMsgsProcessor.Services.Interfaces
{
    public interface IStooqService
    {
        Task<StockValue> GetStockValueByCode(string stock_code);
    }
}
