namespace StockMarket.StockMsgsProcessor.Services.Interfaces
{
    public interface IStooqService
    {
        Task<string> GetStockValueByCode(string stock_code);
    }
}
