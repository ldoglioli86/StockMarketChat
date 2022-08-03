using StockMarket.Chat.Models;

namespace StockMarket.Chat.Services.Interfaces
{
    public interface IChatUserService
    {
        Task<IEnumerable<ChatUser>> GetUsers();
        Task CreateUser(ChatUser user, string password);
        Task<ChatUser> GetUserByUserName(string userName);
    }
}
