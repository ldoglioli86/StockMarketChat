using StockMarket.StockMsgsProcessor.Models;
using StockMarket.StockMsgsProcessor.Services.Interfaces;

namespace StockMarket.StockMsgsProcessor.Services
{
    public class StockMessageProcessor : IStockMessageProcessor
    {
        const string botUser = "Stock Bot";
        private readonly IStooqService _stooqService;
        private readonly IChatHubService _chatHubService;

        public StockMessageProcessor(IStooqService stooqService, IChatHubService chatHubService) {
            _stooqService = stooqService;
            _chatHubService = chatHubService;
        }
        public async void ProcessStockMsg(RabbitStockMsg stockmsg) {
            var stock_code = stockmsg.Message;
            
            try
            {
                string stockValue = await _stooqService.GetStockValueByCode(stock_code);

                var messageToSend = $"{stock_code.ToUpper()} quote is ${stockValue} per share.";

                await _chatHubService.SendMessage(botUser, stockmsg.Room, messageToSend);
            } catch (Exception e)
            {
                if (e.Message.Contains("Unknown Stock Code")) {
                    await _chatHubService.SendMessage(botUser, stockmsg.Room, $"{stock_code.ToUpper()} is not a valid Stock Code.");
                }
                Console.WriteLine(e.Message);
            }
        }
    }
}
