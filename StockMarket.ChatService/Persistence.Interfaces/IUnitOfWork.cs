using StockMarket.Chat.Persistence.Interfaces;

namespace StockMarket.Chat.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IChatUserRepository UserRepository { get; }
        IAuthenticationRepository AuthenticationRepository { get; }
    }
}
