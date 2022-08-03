using StockMarket.Chat.Models;

namespace StockMarket.Chat.Persistence.Interfaces
{
    public interface IChatUserRepository : IRepository<ChatUser>
    {
        Task<ChatUser> CreateUser(ChatUser User, string Password);
        Task<ChatUser> GetUserById(string id);
        Task<IEnumerable<ChatUser>> GetUsers();
        Task<ChatUser> GetUserByUserName(string username);
    }
}
