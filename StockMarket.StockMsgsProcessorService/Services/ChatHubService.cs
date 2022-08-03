using StockMarket.StockMsgsProcessor.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace StockMarket.StockMsgsProcessor.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IConfiguration _configuration;

        public ChatHubService(IConfiguration config)
        {
            _configuration = config;
        }

        public async Task SendMessage(string user, string room, string message) {
            var hubConnection = new HubConnectionBuilder()
                     .WithUrl(_configuration.GetSection("Hub:url").Value)
                     .WithAutomaticReconnect().Build();

            await hubConnection.StartAsync().ContinueWith(task =>
            {
                hubConnection.InvokeAsync("SendMessage", user, room, message);
            });
        }
    }
}
