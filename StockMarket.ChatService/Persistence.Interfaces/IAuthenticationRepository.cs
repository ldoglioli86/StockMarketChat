using StockMarket.Chat.Models;
using Microsoft.AspNetCore.Identity;

namespace StockMarket.Chat.Persistence.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<SignInResult> CheckPasswordSignIn(ChatUser user, string password);
    }
}

