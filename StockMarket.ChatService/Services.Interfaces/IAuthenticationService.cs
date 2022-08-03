using StockMarket.Chat.Models;

namespace StockMarket.Chat.Services.Interfaces
{
    public interface IAuthenticationService
    {
         Task<LoginResponse> Login(string username, string password);
    }
}
