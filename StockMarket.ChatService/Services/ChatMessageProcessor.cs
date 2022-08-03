using System.Text.RegularExpressions;
using StockMarket.Chat.Models;
using StockMarket.Chat.Services.Interfaces;

namespace StockMarket.Chat.Services
{
    public class ChatMessageProcessor : IChatMessageProcessor
    {
        // RegEx Pattern to detect commands
        private const string commandPattern = @"^/[A-za-z]*=";

        private readonly IRabbitStockMsgProducerService _rabbitService;

        public ChatMessageProcessor(IRabbitStockMsgProducerService rabbitService) {
            _rabbitService = rabbitService;
        }

        public void ProccessChatMessage(string message, string room)
        {
            var command = getCommandFromMessage(message);

            if (!string.IsNullOrEmpty(command) && command.Equals("stock")) {
                var stockMessage = new RabbitStockMsg
                {
                    Room = room,
                    Message = getValueForCommand(command, message)
                };
                _rabbitService.SendStockMessage(stockMessage);
            }
        }

        public string getCommandFromMessage(string message)
        {
            var commandMatch = RegexCommandMatch(message);
            if (commandMatch.Success) {
                var fullCommand = commandMatch.Value;
                return fullCommand.Substring(1, fullCommand.Length - 2).ToLower();
            }
            return "";
        }

        private Match RegexCommandMatch(string message)
        {
            return Regex.Match(message, commandPattern);
        }

        public string getValueForCommand(string command, string message)
        {
            return message.Substring(command.Length + 2);
        }
    }
}
