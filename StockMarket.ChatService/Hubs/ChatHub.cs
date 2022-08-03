using StockMarket.Chat.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace StockMarket.Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageProcessor _chatMessageProcessor;

        public ChatHub( IChatMessageProcessor chatMessageProcessor) {
            _chatMessageProcessor = chatMessageProcessor;
        }

        public async Task JoinRoom(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        public async Task SendMessage(string user, string room, string message)
        {
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message, DateTime.Now);

            _chatMessageProcessor.ProccessChatMessage(message, room);
        }
    }
}
