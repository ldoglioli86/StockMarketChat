namespace StockMarket.StockMsgsProcessor.Models
{
    public class RabbitStockMsg
    {
        public string Room { get; set; }
        public string Message { get; set; }
    }
}
