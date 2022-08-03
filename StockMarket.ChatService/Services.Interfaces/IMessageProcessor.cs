namespace StockMarket.Chat.Services.Interfaces
{
    public interface IChatMessageProcessor
    {
        void ProccessChatMessage(string message, string room);
    }
}
