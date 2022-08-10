using StockMarket.Chat.Models;
using StockMarket.Chat.Persistence;
using StockMarket.Chat.Services.Interfaces;

namespace StockMarket.Chat.Services
{
    public class ChatUserService : IChatUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ChatUser>> GetUsers()
        {
            return await _unitOfWork.UserRepository.GetUsers();
        }

        public async Task CreateUser(ChatUser user, string password)
        {
            await _unitOfWork.UserRepository.CreateUser(user, password);
        }


        public async Task<ChatUser> GetUserByUserName(string username)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserName(username);

            return user;
        }
    }
}
