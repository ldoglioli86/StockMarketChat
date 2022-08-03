namespace StockMarket.StockMsgsProcessor.Services.Interfaces
{
    public interface IChatHubService
    {
        Task SendMessage(string user, string room, string message);
    }
}
